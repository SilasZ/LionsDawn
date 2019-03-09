using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 20;
    public float rotspeed = 10f;
	Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
	{
        float forwardspeed = (transform.InverseTransformDirection(rb.velocity)).y;
        

        if (Input.GetKey("w")) {
    	  	rb.AddForce(transform.up * speed);
  		}

 	   	if(Input.GetKey("s")) {
            
            
            if (forwardspeed>0)
            {
                rb.AddForce(transform.up * -speed * 0.5F);
            }
  	      	
    	}

		if(Input.GetKey("d")) {
            // Clockwise
            if (rb.angularVelocity > -30)
            {
                rb.AddForce(transform.right*rotspeed*0.5F);
                rb.AddTorque(-rotspeed);
            }
            
     	   //transform.Rotate(0, 0, -rotspeed); // --> Instead of "transform.Rotate(-1.0f, 0.0f, 0.0f);"
  		}
       
 	   	if(Input.GetKey("a")) {
            // Counter-clockwise
            if (rb.angularVelocity < 30 )
            {
                rb.AddForce(-transform.right*rotspeed*0.5F);
                rb.AddTorque(rotspeed);
            }
                
  	      //transform.Rotate(0, 0, rotspeed); // --> Instead of transform.Rotate(1.0f, 0.0f, 0.0f);
    	}



	}

 

    
}
