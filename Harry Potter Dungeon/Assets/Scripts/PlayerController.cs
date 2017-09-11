using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float PlayerSpeed;
    public float CamSpeed;
    public float Gravity;

    private Rigidbody rb;
    private Quaternion lookCamera;

    private bool jump = false;
    private float nextJump = 0.0f;
	
	void Start ()
    {
        rb = GetComponent<Rigidbody> ();
        lookCamera = Quaternion.identity;        

        Cursor.visible = false;
	}

    void Update ()
    {
        if (Input.GetButton ("Fire2") && Time.time > nextJump && transform.position.y < 0.42f)
        {
            nextJump = Time.time + 0.21f;            
        }    
        jump = Time.time < nextJump;        
    }
	
	void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");            

        Vector3 movement = lookCamera * new Vector3 (moveHorizontal, 0.0f, moveVertical);
        movement.Set (movement.x, jump ? 1.0f : 0.0f, movement.z);
        rb.velocity = movement * PlayerSpeed;
        rb.rotation = Quaternion.identity;

        rb.AddForce (Physics.gravity * Gravity);
    } 
        
    void LateUpdate ()
    {
        float lookHorizontal = Input.GetAxis ("Mouse X");
        float lookVertical = Input.GetAxis ("Mouse Y");
       
        Vector3 lookCam = new Vector3 (-lookVertical, lookHorizontal, 0.0f) * CamSpeed;
        lookCamera *= Quaternion.Euler (lookCam);
        lookCamera = Quaternion.Euler (new Vector3
        (
            ClampAngle (lookCamera.eulerAngles.x, 60.0f),
            lookCamera.eulerAngles.y,
            0.0f
        ));       

        transform.rotation = lookCamera;
    }

    float ClampAngle (float angle, float deltaMax)
    {
        if (angle < 180.0f)
        {
            return Mathf.Clamp (angle, 0, deltaMax);
        }        
        return Mathf.Clamp (angle, 360.0f - deltaMax, 360.0f);        
    }   
}
