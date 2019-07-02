using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour {


    public VoidEvent onStartWalking;
    public VoidEvent onStopWalking;
    public VoidEvent onShooting;
    private PlayerMovement pmove;
    private Animator mAnim;

    bool isSwimming;
    bool isWalking;


    // Use this for initialization
    void Start () {
        pmove = GetComponent<PlayerMovement>();
        mAnim = GetComponentInChildren<Animator>();
	}
	
    public void SetIsSwimming(bool _isSwimming)
    {
        isSwimming = _isSwimming;
        mAnim.SetBool("isSwimming",isSwimming);
        if (_isSwimming)
            StopWalking();
    }

    void StartWalking()
    {
        if (isWalking)
            return;
        mAnim.SetBool("isWalking", true);
        onStartWalking.Invoke();
        isWalking = true;
    }

    void StopWalking()
    {
        if (!isWalking)
            return;
        mAnim.SetBool("isWalking", false);
        onStopWalking.Invoke();
        isWalking = false;
    }

    // Update is called once per frame
    void Update () {

        if (pmove.canMove)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (!isSwimming)
            {

                if (h != 0 || v != 0)
                {
                    StartWalking();
                }
                else
                {
                    StopWalking();
                }
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            mAnim.Play("attack");
            onShooting.Invoke();
        }
    }
    

}
