using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{


    public float speed = 20;
    public float rotspeed = 10f;
    public float hitpointsMax = 100F;
    float hitpointsNow;
    public float waterMax = 100F;
    float waterNow;
	Rigidbody2D rb;
    UIPlayerStatus ui;

    public AudioSource sound;

    void OnCollisionEnter2D(Collision2D collision)  //Plays Sound Whenever collision detected
    {
        sound.volume = GetComponent<Rigidbody2D>().velocity.magnitude * 0.1F;
        sound.Play();
        hitpointsNow = hitpointsNow - 1F*rb.velocity.magnitude;
    }

    private void Start()
    {
        hitpointsNow = hitpointsMax;
        waterNow = waterMax;
        rb = GetComponent<Rigidbody2D>();
        ui = GetComponent<UIPlayerStatus>();
    }

    void RefreshUI()
    {
        ui.PlayerStatusUpdate(hitpointsMax, hitpointsNow, waterMax, waterNow);
    }

    private void Update()
    {
        RefreshUI();
    }

    public void FixedUpdate()
	{
        waterNow = waterNow -0.005F;
        bool engineOn = false;
        

        float forwardspeed = (transform.InverseTransformDirection(rb.velocity)).y;
        

        if (Input.GetKey("w")) {
    	  	rb.AddForce(transform.up * speed);
            engineOn = true;
  		}

 	   	if(Input.GetKey("s")) {
            
            
            if (forwardspeed>0)
            {
                rb.AddForce(transform.up * -speed * 0.5F);
                engineOn = true;
            }
  	      	
    	}

		if(Input.GetKey("d")) {
            // Clockwise
            if (rb.angularVelocity > -30)
            {
                //rb.AddForce(transform.right*rotspeed*0.5F);
                rb.AddTorque(-rotspeed);
                engineOn = true;
            }
            
     	   //transform.Rotate(0, 0, -rotspeed); // --> Instead of "transform.Rotate(-1.0f, 0.0f, 0.0f);"
  		}
       
 	   	if(Input.GetKey("a")) {
            // Counter-clockwise
            if (rb.angularVelocity < 30 )
            {
                //rb.AddForce(-transform.right*rotspeed*0.5F);
                rb.AddTorque(rotspeed);
                engineOn = true;
            }
                
  	      //transform.Rotate(0, 0, rotspeed); // --> Instead of transform.Rotate(1.0f, 0.0f, 0.0f);
    	}

        if (engineOn)
        {
            waterNow = waterNow - 0.02F;
        }



	}

 

    
}
