using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightningFlash : MonoBehaviour {

    public GameObject bossCube;
    public GameObject babies;
    public GameObject babycam;
    public VoidEvent playlightningSound;
    private Image myImage;
    private float lightingTimer = 0.3f;

	// Use this for initialization
	void Start () {
        Destroy(babies);
        Destroy(babycam);
        Instantiate(bossCube, Vector3.zero, Quaternion.identity, null);
        myImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

        if (lightingTimer > 0)
        {
            playlightningSound.Invoke();
            lightingTimer -= Time.deltaTime;
        }
        else
        {
            myImage.enabled = false;
        }

	}
}
