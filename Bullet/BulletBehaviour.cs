using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

    private Rigidbody rbody;
    public ParticleSystem psyst;
    public GameObject explosion;
    public float bulletSpeed;
    public float lifeSpan = 4;

    public System.Action onPlayerHitEnemy;

	// Use this for initialization
	void Start () {

        rbody = GetComponent<Rigidbody>();
        psyst.transform.parent = null;

    }
	
	// Update is called once per frame
	void Update () {

        psyst.transform.position = transform.position;
        
        rbody.velocity = transform.forward * bulletSpeed;

        AgeBullet();

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            onPlayerHitEnemy();
            Instantiate(explosion,transform.position,transform.rotation,null);
            Destroy(other.transform.parent.gameObject);
            Destroy(gameObject);

        }
        if (other.gameObject.tag == "Boss")
        {
            other.GetComponent<BossBehaviour>().currentHitPoints -= 1;
            Instantiate(explosion, transform.position, transform.rotation, null);
            Destroy(gameObject);

        }
    }


   // private void OnCollisionEnter(Collision collision)
    //{
     //   if (collision.gameObject.tag == "Enemy")
      //  {
       //     Destroy(gameObject);
       // }
   // }

    public void AgeBullet()
    {
        if (lifeSpan > 0)
            lifeSpan -= Time.deltaTime;
        if (lifeSpan <= 0)
            Destroy(gameObject);
    }
}
