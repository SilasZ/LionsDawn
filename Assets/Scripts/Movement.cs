using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Camera cam;
    float cameraStartSize;
    public float speed = 20;
    float standardSpeed;
    public float rotspeed = 10f;
    public float hitpointsMax = 100F;
    float hitpointsNow;
    public float waterMax = 100F;
    float waterNow;
    int wood=0;
	Rigidbody2D rb;
    UIPlayerStatus ui;

    

    public int crewMax;
    int crewNow=0;

    public AudioSource sound;
    public ParticleSystem steam;

    public Camera minimap;


    Place[] places;

    void OnCollisionEnter2D(Collision2D collision)  //Plays Sound Whenever collision detected
    {
        sound.volume = GetComponent<Rigidbody2D>().velocity.magnitude * 0.1F;
        sound.Play();
        hitpointsNow = hitpointsNow - 1F*rb.velocity.magnitude;
    }

    private void OnMouseDown()
    {
        var children = gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child.transform != transform)
                child.gameObject.BroadcastMessage("OnMouseDown", options: SendMessageOptions.DontRequireReceiver);
        }
    }

    private void Start()
    {
        hitpointsNow = hitpointsMax;
        waterNow = waterMax;
        rb = GetComponent<Rigidbody2D>();
        ui = GetComponent<UIPlayerStatus>();

        SetPlacesOnStartValues();
        RefreshCrewMax();

        standardSpeed = speed;
        cam = GetComponentInChildren<Camera>();
        cameraStartSize = cam.orthographicSize;

        AddWood(3);
    }

    void SetPlacesOnStartValues()
    {
        places = GetComponentsInChildren<Place>();

        int i=0;
        foreach (Place place in places)
        {
            i++;
            if (i > 3)
            {
                place.gameObject.SetActive(false);
            }
        }
    }

    public void DealDamage(float damage)
    {
        hitpointsNow = hitpointsNow - damage;
    }

    public bool HasEnoughWood(int woodCount)
    {
        if (wood >= woodCount)
        {
            return true;
        }

        return false;
    }

    public void IncreaseWaterMaxBy(float increase)
    {
        waterMax = waterMax + increase;
        waterNow = waterNow + increase;
        ui.IncreaseBarByPercentOfStartValue(Bars.water, 50);
    }

    public void IncreaseHullMaxBy(float increase)
    {
        hitpointsMax = hitpointsMax + increase;
        hitpointsNow = hitpointsNow + increase;
        ui.IncreaseBarByPercentOfStartValue(Bars.life, 50);
    }

    public void IncreaseCrewMaxBy(int i)
    {
        foreach(Place place in places)
        {
            if (!place.gameObject.activeSelf)
            {
                place.gameObject.SetActive(true);
                RefreshCrewMax();
                return;
            }
        }
    }

    void RefreshCrewMax()
    {
        int i=0;
        foreach (Place place in places)
        {
            if (place.gameObject.activeSelf) i++;
        }
        crewMax = i;
        RefreshCrewUINumber();
    }

    public void RefreshCrewNow()
    {
        int i = 0;
        foreach (Place place in places)
        {
            if ((place.gameObject.activeSelf)&&(place.gameObject.GetComponentInChildren<PersonMovement>())) i++;
        }
        crewNow = i;
        RefreshCrewUINumber();
        RefreshProfessionBoni();
    }

    void RefreshCrewUINumber()
    {
        ui.NewCrewCount(crewNow, crewMax);
    }

    void RefreshProfessionBoni()
    {
        speed = standardSpeed;
        cam.orthographicSize = cameraStartSize;

        foreach(PersonMovement person in GetComponentsInChildren<PersonMovement>())
        {
            speed = speed + person.GetSpeedBonus();
            cam.orthographicSize = cam.orthographicSize + person.GetVisionBonus();
        }
    }

    public void AddCrowsNest()
    {

    }

    public void RepairShipComplete()
    {
        hitpointsNow = hitpointsMax;
    }

    public void AddWood(int number)
    {
        wood = wood + number;
        ui.NewWoodCount(wood);
    }

    void RefreshUI()
    {
        ui.PlayerStatusUpdate(hitpointsMax, hitpointsNow, waterMax, waterNow);
    }

    public void RefillWaterTank()
    {
        waterNow = waterMax;
    }

    private void Update()
    {
        RefreshUI();
        RefreshMinimapPosition();
    }

    private void RefreshMinimapPosition()
    {
        minimap.transform.position = transform.position+new Vector3(0,0,-10);
    }

    public void FixedUpdate()
	{
        if(hitpointsNow<1 || waterNow < 1)
        {
            ui.ShowDeadText();
            steam.Stop();
            return;
        }
        
        waterNow = waterNow -0.005F;
        bool engineOn = false;
        

        float forwardspeed = (transform.InverseTransformDirection(rb.velocity)).y;
        



        //steam.startSpeed = forwardspeed/4+3;
        var main = steam.main;
        main.startSpeed = forwardspeed / 4 + 3;


        if (Input.GetKey("w")) {
    	  	rb.AddForce(transform.up * speed);
            engineOn = true;
            var emission = steam.emission;
            emission.rateOverTime = 40;
        }
        else
        {
            var emission = steam.emission;
            emission.rateOverTime = 10;
        }

 	   	if(Input.GetKey("s")) {
            
            
            if (forwardspeed>-5F)
            {
                rb.AddForce(transform.up * -speed * 0.5F);
                engineOn = true;
            }
  	      	
    	}

		if(Input.GetKey("d")) {
            // Clockwise
            if (rb.angularVelocity > -50)
            {
                //rb.AddForce(transform.right*rotspeed*0.5F);
                rb.AddTorque(-rotspeed);
                engineOn = true;
            }
            
     	   //transform.Rotate(0, 0, -rotspeed); // --> Instead of "transform.Rotate(-1.0f, 0.0f, 0.0f);"
  		}
       
 	   	if(Input.GetKey("a")) {
            // Counter-clockwise
            if (rb.angularVelocity < 50 )
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
