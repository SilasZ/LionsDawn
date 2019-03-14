using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum Profession {normalDude, lookOut, sailor};

public class PersonMovement : MonoBehaviour
{
    public Sprite normalDudeSprite;
    public Sprite lookOutSprite;
    public Sprite sailorSprite;

    public float beamRadius;
    public GameObject beam;
    private float maxDistance = 10;
    Movement player;

    Profession profession;
    float speedBonusAll = 0;
    float speedBonusSailor = 4;
    float visionBonusAll = 0;
    float visionBonusLookOut = 2;

    public Text pickUpText;
    

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Movement>();
        SetProfession();
    }

    public Profession GetProfession()
    {
        return profession;
    }
    
    private void SetProfession()
    {
        float f = UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(Profession)).Length);
        if (f < 3) profession = Profession.sailor;
        if (f < 2) profession = Profession.lookOut;
        if (f < 1) profession = Profession.normalDude;

        SetProfessionImage();
        SetProfessionBonus();
    }

    private void SetProfessionImage()
    {
        if (profession == Profession.normalDude) GetComponent<SpriteRenderer>().sprite = normalDudeSprite;
        if (profession == Profession.lookOut) GetComponent<SpriteRenderer>().sprite=lookOutSprite;
        if (profession == Profession.sailor) GetComponent<SpriteRenderer>().sprite = sailorSprite;
    }

    void SetProfessionBonus()
    {
        if (profession == Profession.sailor) speedBonusAll = speedBonusSailor;
        if (profession == Profession.lookOut) visionBonusAll = visionBonusLookOut;
    }

    public float GetSpeedBonus()
    {
        return speedBonusAll;
    }
    public float GetVisionBonus()
    {
        return visionBonusAll;
    }




    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        StartCoroutine(Beam()); 
        
    }

    private IEnumerator Beam()
    {
        var places = FindObjectsOfType<Place>().Where(a => (!transform.parent || a.transform.parent != transform.parent.parent) && a.transform.childCount == 0);
        var nearestPlace = places.OrderBy(a => Vector2.Distance(a.transform.position, transform.position)).FirstOrDefault();
        if (nearestPlace && Vector2.Distance(nearestPlace.transform.position, transform.position) < maxDistance)
        {
            Instantiate(beam, transform.position - Vector3.forward, Quaternion.identity);
            Instantiate(beam, nearestPlace.transform.position - Vector3.forward, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
            nearestPlace.GetComponent<Place>().AddPerson(this);

            player.RefreshCrewNow();
            StartCoroutine(SayPickUpText());
        }      
    }

    float pickUpTextShowingTime = 5;
    private IEnumerator SayPickUpText()
    {
        if (GetComponentInParent<Movement>())
        {
            pickUpText.enabled = true;
            GetComponentInChildren<Canvas>().transform.rotation = GetComponentInParent<Movement>().transform.rotation;
            yield return new WaitForSeconds(pickUpTextShowingTime);
            pickUpText.enabled = false;
            pickUpTextShowingTime = 3;
            pickUpText.text = "John, Sailor (Speed +4)";
        }
        else
        {
            pickUpText.enabled = false;
        }
        //yield return new WaitForSeconds(0);
    }
}
