using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShoot : MonoBehaviour {

    public GameObject myBullet;
    public bool isShotPrimed = false;
    public float bulletSpeed = 50;
    public int bulletCost = 2;
    private PlayerMovement pmove;
    public IntEvent onPlayerShoot;

    public PlayerCollector playerCollector;

    public VoidEvent onPlayerHitEnemy;

	// Use this for initialization
	void Start () {
        pmove = GetComponent<PlayerMovement>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (pmove.canShoot && playerCollector.GetInventoryCount() >= bulletCost)
        Shoot();

	}

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isShotPrimed = true;
        }

        if (Input.GetButtonUp("Fire1") && isShotPrimed)
        {
            GameObject shotBullet = Instantiate(myBullet, transform.position + (transform.forward * 2),transform.rotation,null);
            var bulletBehavior = shotBullet.GetComponent<BulletBehaviour>();
            bulletBehavior.bulletSpeed = bulletSpeed;
            bulletBehavior.onPlayerHitEnemy = onPlayerHitEnemy.Invoke;
            isShotPrimed = false;
            onPlayerShoot.Invoke(-bulletCost);
        }
    }
}
