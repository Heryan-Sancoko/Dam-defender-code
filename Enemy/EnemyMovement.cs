using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Wandering,
    ChasingPlayer,
    ExitingStage
}


public class EnemyMovement : MonoBehaviour {



    public float rotationSpeed = 100;
    public float velocitySpeed = 10;
float maxRadius = 1;


    public GameObject targetPlayer;

    float rndSeed;

    EnemyState state = EnemyState.Wandering;
    

    bool isPaused = false;

public    bool debug;


    void Start () {
        transform.tag = "Enemy";
        var manager = GetComponentInParent<GameManager>();

        maxRadius = manager == null ? 30 : manager.GameRadius;
        EnterStage();
        rndSeed = Random.Range(0f, 10000f);
	}
	
	// Update is called once per frame
	void Update () {
        if (isPaused)
            return;
        switch (state)
        {
            case EnemyState.Wandering:
                HandleWander();
                break;
            case EnemyState.ExitingStage:
                HandleExitStage();
                break;

            case EnemyState.ChasingPlayer:
                HandleChasePlayer();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Bullet")
            GameObject.Destroy(gameObject);
        if (other.transform.tag == "Flood")
            Destroy(gameObject);
        if (other.transform.tag == "KillEnemy")
            Destroy(gameObject);

    }

    public void Pause()
    {
        isPaused = true;
    }

    public void EnterStage()
    {
        var theta = Random.Range(0, Mathf.PI * 2);
        transform.position = new Vector3(Mathf.Cos(theta), 0, Mathf.Sin(theta)) *maxRadius;
        var dirCenter = -transform.position;
        transform.rotation = Quaternion.LookRotation(dirCenter, Vector3.up);
    }

    void HandleWander()
    {
            if (Vector3.Distance(transform.position, Vector3.zero) > maxRadius)
            {
                var dirCenter = Vector3.zero - transform.position;
                RotateToward(dirCenter);
            }
            else
            {
                float rotation = rotationSpeed * Time.deltaTime * ((Mathf.PerlinNoise(Time.time, rndSeed) * 2) - 1);
                transform.Rotate(Vector3.up * rotation);
            }
        float velocity = velocitySpeed * Time.deltaTime * Mathf.PerlinNoise(Time.time, rndSeed + 100000);
        transform.Translate(Vector3.forward * velocity);
    }
    
    void HandleChasePlayer()
    {
        var dirPlayer = targetPlayer.transform.position - transform.position;
        RotateToward(dirPlayer);
        TravelFast();
    }

    void RotateToward(Vector3 direction)
    {
        var flatDirection = new Vector3(direction.x, 0, direction.z);
        var rotPlayer = Quaternion.LookRotation(flatDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotPlayer, rotationSpeed * Time.deltaTime);
    }

    void TravelFast()
    {
        float velocity = velocitySpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * velocity);
    }

    void HandleExitStage()
    {
        Vector3 dirExit = transform.position;
        transform.LookAt(dirExit, Vector3.up);
        TravelFast();
        if (Vector3.Magnitude(transform.position) > maxRadius)
            GameObject.Destroy(gameObject);

    }

    public void ExitStage()
    {
        state = EnemyState.ExitingStage;
    }

    public void BeginChasingPlayer(GameObject player)
    {
        if (state == EnemyState.ExitingStage)
            return;
        state = EnemyState.ChasingPlayer;
        targetPlayer = player;
        if(debug)
        Debug.Log("begin chasing player");
    }

    public void EndChasingPlayer()
    {
        if (debug)
            Debug.Log("end chasing player");
        if (state == EnemyState.ExitingStage)
            return;
        state = EnemyState.Wandering;
    }
}
