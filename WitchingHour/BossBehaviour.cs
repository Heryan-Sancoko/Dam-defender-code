using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehaviour : MonoBehaviour {

    public GameObject myPlayer;
    public GameObject mySnakes;
    public float movespeed;
    public int maxHitPoints;
    public int currentHitPoints;
    private Rigidbody rbody;
    private float snakeTimer = 3f;

    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (myPlayer != null)
        Move();

        if (snakeTimer > 0)
        {
            snakeTimer -= Time.deltaTime;
        }
        else if (snakeTimer <= 0)
        {
            Instantiate(mySnakes, transform.position, transform.rotation, null);
            snakeTimer = 3f;
        }

        if (currentHitPoints <= 0)
        {
            SceneManager.LoadScene(2);
        }

	}

    public void Move()
    {
        transform.LookAt(myPlayer.transform.position);
        rbody.velocity = transform.forward * movespeed;

    }

}
