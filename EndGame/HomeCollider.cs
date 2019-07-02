using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeCollider : MonoBehaviour {

    public SkinnedMeshRenderer mySkin;
    public CameraBehaviour mCamBehave;
    public Transform camTransform;
    public Transform camHolderTransform;
    public Transform newCamHolderTransform;
    public GameObject fakeBeaver;
    private bool isInHouse = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (isInHouse && Input.GetMouseButtonDown(0))
            SceneManager.LoadScene(0, LoadSceneMode.Single);

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isInHouse = true;
            mySkin.enabled = false;
            mCamBehave.enabled = false;
            camTransform.localPosition = Vector3.zero;
            camTransform.rotation = Quaternion.identity;
            fakeBeaver.SetActive(true);
            camHolderTransform.position = newCamHolderTransform.position;
            camHolderTransform.rotation = newCamHolderTransform.rotation;
        }
    }
}
