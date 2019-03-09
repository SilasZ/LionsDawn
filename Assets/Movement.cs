using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 100;
    public float rotspeed = 0.75f;
	public Rigidbody2D rb;

	public void FixedUpdate()
	{	    

	    if(Input.GetKey("w")) {
    	  	rb.AddForce(transform.up * speed);
  		}

 	   	if(Input.GetKey("s")) {
  	      	rb.AddForce(transform.up * -speed);
    	}

		if(Input.GetKey("d")) {
    	    // Clockwise
     	   transform.Rotate(0, 0, -rotspeed); // --> Instead of "transform.Rotate(-1.0f, 0.0f, 0.0f);"
  		}

 	   	if(Input.GetKey("a")) {
  	      // Counter-clockwise
  	      transform.Rotate(0, 0, rotspeed); // --> Instead of transform.Rotate(1.0f, 0.0f, 0.0f);
    	}

	}

 

    
}
