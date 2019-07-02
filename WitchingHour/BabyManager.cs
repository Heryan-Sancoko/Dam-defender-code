using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyManager : MonoBehaviour {

    public GameObject myPlayer;
    public GameObject babyBeaver;
    public float spawnTimer = 3;
    public int babiesSpawned = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (spawnTimer > 0 && babiesSpawned != 3)
        {
            spawnTimer -= Time.deltaTime;
        }

        if (spawnTimer <= 0 && babiesSpawned != 3)
        {
            GameObject spawnBaby = Instantiate(babyBeaver, transform.position, Quaternion.identity, null);
            //            spawnBaby.GetComponent<BabyBehaviour>().myPlayer = myPlayer;
            spawnBaby.GetComponent<BabyBehaviour>().myPlayer = GameObject.FindGameObjectWithTag("Player");

            babiesSpawned++;
            spawnTimer = 3;
        }

        if (babiesSpawned >= 3)
        {
            Destroy(gameObject);
        }
		
	}
}
