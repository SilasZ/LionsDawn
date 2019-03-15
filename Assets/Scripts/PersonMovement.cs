using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum Profession {NormalDude, LookOut, Sailor};

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
    float speedBonusSailor = 3;
    float visionBonusAll = 0;
    float visionBonusLookOut = 2;

    public Text pickUpText;
    String Name="";
    String RescueSentence="";
    String ProfessionBonusInfo="";


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Movement>();
        SetProfession();
        SetStrings();
        SetPickUpText();
    }

    public Profession GetProfession()
    {
        return profession;
    }
    
    private void SetProfession()
    {
        int i = UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(Profession)).Length);
        if (i < 3) profession = Profession.Sailor;
        if (i < 2) profession = Profession.LookOut;
        if (i < 1) profession = Profession.NormalDude;

        SetProfessionImage();
        SetProfessionBonus();
    }

    void SetStrings()
    {
        SetName();
        SetRescueSentence();
        SetProfessionBonusInfo();
    }

    void SetName()
    {
        String[] names = new string[] {"Tom","John","Silas","Fabian","Basti","Lukas","Jan","Adrian","Julian","Philip","Neil","Hanson","Jake","Simon"};
        int i = UnityEngine.Random.Range(0, names.Length);
        Name = names[i];
    }

    void SetRescueSentence()
    {
        String[] sentences = new string[] {
            "Thank you for saving me!",
            "Can you bring me somewhere safe?",
            "Please, I need water",
            "How did you find me?",
            "Oh god, I thought I was dead",
            "You saved my life, how can I ever thank you?",
            "How did I even get here?",
            "What happened to this place?",
            "Thanks for the ride mate",
            "I miss my cow, did you see her?",
            "I am so thirsty",
            "I saw a tornado the other day!",
            "Damn, I got sand in my shoes"};

        int i = UnityEngine.Random.Range(0, sentences.Length);
        RescueSentence = sentences[i];
    }

    void SetProfessionBonusInfo()
    {
        if (profession == Profession.Sailor) ProfessionBonusInfo = "(Speed +" + speedBonusSailor+")";
        if (profession == Profession.LookOut) ProfessionBonusInfo = "(Vision +" + visionBonusLookOut+")";
    }

    void SetPickUpText()
    {
        pickUpText.text ="\""+RescueSentence+"\"\n"+Name+", " + profession + " "+ProfessionBonusInfo;
    }

    private void SetProfessionImage()
    {
        if (profession == Profession.NormalDude) GetComponent<SpriteRenderer>().sprite = normalDudeSprite;
        if (profession == Profession.LookOut) GetComponent<SpriteRenderer>().sprite=lookOutSprite;
        if (profession == Profession.Sailor) GetComponent<SpriteRenderer>().sprite = sailorSprite;
    }

    void SetProfessionBonus()
    {
        if (profession == Profession.Sailor) speedBonusAll = speedBonusSailor;
        if (profession == Profession.LookOut) visionBonusAll = visionBonusLookOut;
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
            pickUpTextShowingTime = 2;
            pickUpText.text = Name + ", " + profession + " " + ProfessionBonusInfo;
        }
        else
        {
            pickUpText.enabled = false;
        }
        //yield return new WaitForSeconds(0);
    }
}
