using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBehaviour : MonoBehaviour {

    private float spawnHeight; // to constrain height of babies
    public GameObject myPlayer;
    private PlayerCollector mPlayerCollector;
    private SkinnedMeshRenderer mSkin;
    private PlayerHealth mPlayerHealth;
    public float movespeed;
    private int resourceType;
    public float homeTimer = 1;
    private bool isDelivering = true;
    private Rigidbody rbody;



	// Use this for initialization
	void Start () {
        spawnHeight = transform.position.y;
        rbody = GetComponent<Rigidbody>();
        mPlayerCollector = myPlayer.GetComponent<PlayerCollector>();
        mPlayerHealth = myPlayer.GetComponent<PlayerHealth>();
        mSkin = GetComponentInChildren<SkinnedMeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

        if (myPlayer != null)
        {

            


            if (isDelivering)
            {
                transform.LookAt(myPlayer.transform.position);
                
                if (Vector3.Distance(transform.position, myPlayer.transform.position) < 1)
                {
                    isDelivering = false;
                    resourceType = Random.Range(1, 100);
                    if (resourceType % 2 == 0)
                    {
                        mPlayerCollector.inventory += 3;
                        mPlayerCollector.onInventoryUpdate.Invoke(mPlayerCollector.inventory);
                    }
                    else if (resourceType % 2 != 0)
                    {
                        if (mPlayerHealth.health < 100)
                        {
                            mPlayerHealth.AddHealth();
                        }
                        else
                        {
                            mPlayerCollector.inventory += 3;
                            mPlayerCollector.onInventoryUpdate.Invoke(mPlayerCollector.inventory);
                            mPlayerCollector.onInventoryAddedAtPosition.Invoke(mPlayerCollector.transform.position);
                        }
                    }
                }

            }
            if (!isDelivering)
            {
                transform.LookAt(Vector3.zero);
                if (Vector3.Distance(transform.position, Vector3.zero) < 1)
                {
                    mSkin.enabled = false;
                    homeTimer -= Time.deltaTime;

                    if (homeTimer <= 0)
                    {
                        isDelivering = true;
                        homeTimer = Random.Range(1f,3f);
                        mSkin.enabled = true;
                    }
                }
            }


            rbody.velocity = transform.forward * movespeed;

            transform.position = new Vector3(transform.position.x, spawnHeight, transform.position.z);
        }

	}
}
