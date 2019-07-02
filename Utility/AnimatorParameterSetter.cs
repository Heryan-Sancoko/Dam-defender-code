using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorParameterSetter : MonoBehaviour {

    // Use this for initialization

    Animator animator;
    void Start () {
        animator = GetComponent<Animator>();
	}
	

public    void SetBool(string name,bool value)
    {
        animator.SetBool(name, value);
    }
    
	// Update is called once per frame
	void Update () {
		
	}
}
