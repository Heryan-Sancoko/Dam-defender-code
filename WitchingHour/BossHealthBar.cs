using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour {

    public BossBehaviour mBoss;
    private Image healthBar;
    public float health;
    public Text myText;
    public int randMsg;
    private float msgTimer = 0;

	// Use this for initialization
	void Start () {
        healthBar = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

        health = (float)mBoss.currentHitPoints / mBoss.maxHitPoints;
        healthBar.fillAmount = health;

        msgTimer += Time.deltaTime;

        if (msgTimer > 2)
            GimmeRandNumber();

        switch (randMsg)
        {
            case 1:
                myText.text = "GET OUT!!!";
                break;
            case 2:
                myText.text = "GO AWAY!!!";
                break;
            case 3:
                myText.text = "DIE!!!";
                break;
            case 4:
                myText.text = "I HATE YOU!!!";
                break;
            default:
                myText.text = "GET OUT!!!";
                break;
        }
		
	}

    void GimmeRandNumber()
    {
        randMsg = Random.Range(1, 4);
        msgTimer = 0;
    }
}
