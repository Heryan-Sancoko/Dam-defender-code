using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState
{
    Idle,
    Walking,
    Swimming
}


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public LayerMask myRayMask;
    public bool canShoot = true;
    public bool canMove = true;
    public float movespeed = 10;
    public float movespeedCap = 1;
    Camera myCam;
    private Vector3 rayHit;
    public Vector3 mousePosTest;
    

    public bool debug;

    private Rigidbody rbody;

    // Use this for initialization
    void Start()
    {
        transform.tag = "Player";
        rbody = GetComponent<Rigidbody>();
        rbody.useGravity = false;
//        rbody.drag = 1;
        myCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }


    public void SetMovement(bool _canMove)
    {
        canMove = _canMove;
    }
    // Update is called once per frame
    void Update()
    {
        if(canMove)
            Move();


    }


    private void LateUpdate()
    {
        if(canMove)
        Rotate();
    }

    private void Move()
    {
        //get the state of the buttons for movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");



        //add force depending on which buttons you press
        rbody.AddForce(movespeed * h , 0, movespeed * v, ForceMode.VelocityChange);



        //clamp your movementspeed
        rbody.velocity = Vector3.ClampMagnitude(rbody.velocity, movespeedCap);
    }

    private void Rotate()
    {
        RaycastHit hit;
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100000, myRayMask))
        {
            rayHit = hit.point;
            //get current rotation
            Quaternion currentRotation = transform.rotation;
            //get the player to look at the mouse
            transform.LookAt(rayHit);
            //get the new rotation
            Quaternion lookatRotation = transform.rotation;
            lookatRotation.x = currentRotation.x;
            lookatRotation.z = currentRotation.z;

            transform.rotation = lookatRotation;
        }
        


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if(debug)
        Gizmos.DrawSphere(rayHit, 2);
    }
}
