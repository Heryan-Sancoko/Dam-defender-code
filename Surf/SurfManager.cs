using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfManager : MonoBehaviour {


    public GameObject surfboardPrefab;
    public GameObject spoopySurfboardPrefab;
    public CycleManager myDayNightCycle;

    //    AudioSource audioSource;

    public VoidEvent playerGoesSwimming;
    public VoidEvent playerStopsSwimming;
    private void Awake()
    {
  //      audioSource = GetComponentInChildren<AudioSource>();   
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Enemy")
        {
            if (myDayNightCycle.cycleCount < 3)
            {
                GameObject.Instantiate(surfboardPrefab, other.transform);
            }
            else
            {
                GameObject.Instantiate(spoopySurfboardPrefab, other.transform);
            }
            var anim =  other.transform.parent.GetComponentInChildren<Animator>();
            if(anim!= null)
                anim.SetBool("isSurfing", true);

        }else if(other.transform.tag == "Player")
        {
            playerGoesSwimming.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            var anim = other.transform.parent.GetComponentInChildren<Animator>();
            if (anim != null)
                anim.SetBool("isSurfing", false);

            foreach (Transform child in other.transform)
            {
                if (child.tag == "Surfboard")
                {
                    Destroy(child.gameObject);
                }
            }
        }
        else if (other.transform.tag == "Player")
        {
            playerStopsSwimming.Invoke();
        }

    }
}
