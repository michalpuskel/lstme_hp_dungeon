using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;
	
	void Start ()
    {
        rb = GetComponent<Rigidbody> ();        
	}
	
	void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);       
        rb.velocity = movement * speed;       
        //rb.rotation = Quaternion.Euler (0.0f, 0.0f, 0.0f); // variable fix here !!!        
    } 
    
    void LateUpdate ()
    {
        float lookHorizontal = Input.GetAxis ("Mouse X");
        float lookVertical = Input.GetAxis ("Mouse Y");

        transform.Rotate (new Vector3 (-lookVertical, lookHorizontal, 0.0f));        
        transform.rotation = Quaternion.Euler
        (
            Mathf.Clamp (transform.rotation.eulerAngles.x, -60.0f, 60.0f),
            //      Mathf.Clamp (transform.rotation.eulerAngles.y, -90.0f, 0.0f),
            //transform.rotation.eulerAngles.x,
            transform.eulerAngles.y,
            0.0f
        );   
    }       
}
