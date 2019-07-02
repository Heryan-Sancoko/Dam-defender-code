using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public Transform cameraFocus;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

        transform.position = new Vector3(cameraFocus.position.x, transform.position.y, cameraFocus.position.z);

	}
}
