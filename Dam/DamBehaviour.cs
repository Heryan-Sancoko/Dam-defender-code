using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamBehaviour : MonoBehaviour {

    //public IntEvent changeDamHealth;
    private float defaultPassageOfTime;
    public ValueDisplay damStrengthDisplay;
    public int damHealth = 0;
    public GameObject finishDayCanvas;
    public CycleManager myDayNightCycle;
    public bool isSleeping = false;
    public GameObject titleCard;
    public GameObject lightningCanvas;
    public GameObject myFlood;
    public PlayerCollector pcollector;
    private Collider pHitBox;
    [SerializeField]
    private GameObject myPlayer;
    private PlayerMovement pmove;
    private Vector3 myFloodOriginalPos;
    private LineRenderer homewardLine;

    public IntEvent onDamHealthUpdate;
    public VoidEvent onBossBattleStart;
    bool bossBattleHasStarted;


    // Use this for initialization
    void Start () {
        //Get flood mesh's original location off screen.
        //Flood mesh will be reset to this position at the start of every day.
        myFloodOriginalPos = myFlood.transform.position;
        myFlood.transform.parent = null;
        //disable the title card which appears at the start of every day.
        titleCard.SetActive(false);
        defaultPassageOfTime = myDayNightCycle.hoursPerSecond;
        homewardLine = GetComponent<LineRenderer>();
        homewardLine.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        //changeDamHealth.Invoke(damHealth);
        damStrengthDisplay.SetText(damHealth);

        if (damHealth < 0)
        {
            transform.root.GetComponent<GameManager>().GameOver();
            Destroy(gameObject);
        }

        if (myPlayer != null)
            pmove = myPlayer.GetComponent<PlayerMovement>();

        if (myDayNightCycle.cycleCount < 3) //Remove this line to remove boss state in game
        {

            //if the player chooses to sleep, they advance time quickly
            if (isSleeping)
            {
                //disable the movement and shooting scripts
                pmove.canMove = false;
                pmove.canShoot = false;
                //advance time faster
                myDayNightCycle.hoursPerSecond = 6;

                //if the time is after 12am and before 4am
                //start the flood
                if (myDayNightCycle.currentHour <= 4)
                {
                    //Time fast forwards
                    myDayNightCycle.hoursPerSecond = 0.5f;
                    //position of flood mesh is lerped to the center of the hut, covering the map
                    myFlood.transform.position = Vector3.Lerp(myFlood.transform.position, new Vector3(0, myFlood.transform.position.y + 2, 0), 0.01f);
                    //once the flood mesh is over the map
                    if (myDayNightCycle.currentHour > 2)
                    {
                        //activate the title card which lets player know they
                        //will soon resume tha game in the next day
                        titleCard.SetActive(true);
                    }
                }
                //when the time gets past 4am
                else if (myDayNightCycle.currentHour > 4 && myDayNightCycle.currentHour <= 5)
                {
                    pHitBox.enabled = true;

                    //disable title card
                    titleCard.SetActive(false);
                    //time flows normally again
                    myDayNightCycle.hoursPerSecond = defaultPassageOfTime; // current normal time is set to 0.5 which seems a bit fast.
                                                                           //reset the flood mesh to be outside the map
                    myFlood.transform.position = myFloodOriginalPos;
                    //enable the movement and shooting scripts
                    pmove.canMove = true;
                    pmove.canShoot = true;
                    //resume play
                    isSleeping = false;
                }
            }
            else //if not sleeping
            {
                if (myDayNightCycle.currentHour <= 4)
                {
                    //time speed is normal instead of sped up
                    myDayNightCycle.hoursPerSecond = defaultPassageOfTime;
                    //position of flood mesh is lerped to the center of the hut, covering the map
                    myFlood.transform.position = Vector3.Lerp(myFlood.transform.position, new Vector3(0, myFlood.transform.position.y + 2, 0), 0.01f);
                    Debug.Log("there should be water here");
                    //----------------------Here the player should die when they touch the flood-----------------//
                    //...However, we will code for the next day in case something happens.
                    if (myDayNightCycle.currentHour > 2)
                    {
                        //activate the title card which lets player know they
                        //will soon resume the game in the next day
                        titleCard.SetActive(true);
                    }
                }
                //when the time gets past 4am
                else if (myDayNightCycle.currentHour > 4 && myDayNightCycle.currentHour <= 5)
                {
                    //disable title card
                    titleCard.SetActive(false);
                    //time flows normally again
                    myDayNightCycle.hoursPerSecond = defaultPassageOfTime; // current normal time is set to 1 already, but set it to 1 again  just in case.
                                                                           //reset the flood mesh to be outside the map
                    myFlood.transform.position = myFloodOriginalPos;
                    //resume play

                }

                if (myDayNightCycle.currentHour > 18)
                {
                    homewardLine.enabled = true;
                    homewardLine.SetPosition(0, myPlayer.transform.position);
                    homewardLine.SetPosition(1, transform.position);
                }
                else
                {
                    homewardLine.enabled = false;
                }
            }
        }



        if (myDayNightCycle.cycleCount == 3 && myDayNightCycle.currentHour >= 3) //Remove this line to remove boss state in game
        {
            myDayNightCycle.defaultTimePassage = 0;
            myDayNightCycle.currentHour = 3;
            titleCard.SetActive(false);
            lightningCanvas.SetActive(true);
            if (!bossBattleHasStarted)
            {
            bossBattleHasStarted = true;
            onDamHealthUpdate.Invoke(0);            
            onBossBattleStart.Invoke();
            }
        }

    }
    
    

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            pHitBox = other.GetComponent<Collider>();
            pcollector = other.GetComponent<PlayerCollector>();
            myPlayer = other.gameObject.transform.parent.gameObject;


            if (myDayNightCycle.currentHour > 18)
            {
                if (myPlayer != null)
                {
                    pmove = myPlayer.GetComponent<PlayerMovement>();
                    if (pmove != null)
                        pmove.canShoot = false;
                }
                finishDayCanvas.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Flood")
        {
//            Debug.Log("flood came on cycle " + myDayNightCycle.cycleCount);
//seems to skip cycle 1
            if (myDayNightCycle.cycleCount == 1)
                damHealth -= 10;
            else if (myDayNightCycle.cycleCount == 2)
                damHealth -= 15;
            else if (myDayNightCycle.cycleCount >= 3)
                damHealth -= 20;
            onDamHealthUpdate.Invoke(damHealth);
        }
       // if (other.tag == "Player"&& myDayNightCycle.currentHour > 18)
        //{
         //   SleepTime();
       // }
    }

    private void OnTriggerExit(Collider other)
    {



        if (myPlayer)
        {
            if (myPlayer != null)
            {
                pmove = myPlayer.GetComponent<PlayerMovement>();
                if (pmove != null)
                    pmove.canShoot = true;
            }
            finishDayCanvas.SetActive(false);
        }
    }

    //Code for the buttons
    public void SleepTime()
    {
        homewardLine.enabled = false;
        pHitBox.enabled = false;
        damHealth += pcollector.inventory;
        onDamHealthUpdate.Invoke(damHealth);
        pcollector.ClearInventory();
        isSleeping = true;
        finishDayCanvas.SetActive(false);
        
    }

    public void TheWitchingHour()
    {

    }
}
