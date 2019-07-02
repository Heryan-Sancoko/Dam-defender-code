using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestJuiceScript : MonoBehaviour {

    private float lifetime = 3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.up, 1);

        lifetime -= Time.deltaTime;

        if (lifetime < 0)
        {
            Destroy(gameObject);
        }

	}
}
