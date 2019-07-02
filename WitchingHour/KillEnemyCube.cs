using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyCube : MonoBehaviour {


    private float lifeTimer = 2;
    public GameObject myPlayer;
    public Transform spawnBossHere;
    public GameObject myBoss;
    public GameObject babyManager;
    private bool hasSpawnedBoss = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        lifeTimer -= Time.deltaTime;

        if (lifeTimer <= 0)
            Destroy(gameObject);

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Collectable")
            Destroy(other.transform.parent.gameObject);
        if (other.tag == "HarvestJuice")
            Destroy(other.transform.root.gameObject);
        if (other.tag == "Player")
        {
            myPlayer = other.gameObject;

            if (!hasSpawnedBoss)
            {
                GameObject spawnBoss = Instantiate(myBoss, spawnBossHere.position, Quaternion.identity);
                spawnBoss.GetComponent<BossBehaviour>().myPlayer = myPlayer;

                GameObject spawnBM = Instantiate(babyManager, Vector3.zero, Quaternion.identity);
                spawnBM.GetComponent<BabyManager>().myPlayer = myPlayer;

                hasSpawnedBoss = true;

            }
        }
    }
}
