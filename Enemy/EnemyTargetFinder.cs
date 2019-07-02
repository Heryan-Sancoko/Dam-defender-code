using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetFinder : MonoBehaviour {

    
    public GameObjectEvent onPlayerFound;
    public VoidEvent onPlayerLost;

    public bool debug;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            onPlayerFound.Invoke(other.gameObject);
            if (debug)
                Debug.Log("player found");
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            onPlayerLost.Invoke();
            if (debug)
                Debug.Log("player lost");
        }
    }

   
}
