using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour {

    // Use this for initialization
    [HideInInspector]
    public int health = 100;
    private bool isInDam;
    private bool isAlive = true;
  
    public VoidEvent onHealthZero;
    public IntEvent onHealthDecrease;
    public Vector3Event onHealthIncreaseAtPos;
    public Vector3Event onHealthDecreaseAtPos;
    public IntEvent onHealthChanged;

    private Rigidbody rbody;
    private float hitCooldown = 1;

    void Start () {
        rbody = transform.parent.GetComponent<Rigidbody>();
        onHealthChanged.Invoke(health);
	}

 

    void OnTriggerEnter(Collider other)
    {
        if (hitCooldown >= 1)
        {
            if (other.transform.tag == "Enemy" || other.transform.tag == "Boss")
            {
                ChangeHealth(-10);
                rbody.AddExplosionForce(100, other.transform.position, 500, 0, ForceMode.VelocityChange);
                hitCooldown = 0;
            }
        }

        if (other.transform.tag == "Flood" && !isInDam)
            ChangeHealth(-100);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Dam")
        {
            isInDam = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Dam")
        {
            isInDam = false;
        }
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth;
        onHealthChanged.Invoke(health);

    }

    public void AddHealth()
    {
        health += 10;
        onHealthChanged.Invoke(health);
        onHealthIncreaseAtPos.Invoke(transform.position);
    }

    public void ChangeHealth(int deltaHealth)
    {
        health += deltaHealth;
        onHealthChanged.Invoke(health);
        if (deltaHealth < 0)
        {
            onHealthDecrease.Invoke(health);
            onHealthDecreaseAtPos.Invoke(transform.position);
        }
        if (health <= 0)
            onHealthZero.Invoke();
    }

    private void Update()
    {
        if (hitCooldown < 1)
            hitCooldown += Time.deltaTime;
    }

}
