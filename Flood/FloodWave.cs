using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FloodWave : MonoBehaviour {



    
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            Destroy(other.transform.parent.gameObject);
    }
}
