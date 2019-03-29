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
    int speedBonusAll = 0;
    int speedBonusSailor = 4;
    int visionBonusAll = 0;
    int visionBonusLookOut = 3;

    public Text pickUpText;
    String Name="";
    String RescueSentence="";
    String ProfessionBonusInfo="";
    String ProfessionText = "";

    //hovering over me-------------------------------------------
    HoverUI hoverInterface;

    public bool tutorialHuman = false;

    private void OnMouseEnter()
    {
        if (GetComponentInParent<Place>())
        {
            hoverInterface.show();
            hoverInterface.hoverText.text = Name + " " + ProfessionText + " " + ProfessionBonusInfo;
        }
    }

    private void OnMouseExit()
    {
        hoverInterface.hide();
    }
    //------------------------------------------------------------


    // Start is called before the first frame update
    void Start()
    {
        speedBonusSailor = UnityEngine.Random.Range(1,5);
        visionBonusLookOut = UnityEngine.Random.Range(1, 4);

        player = FindObjectOfType<Movement>();
        SetProfession();
        SetStrings();
        SetPickUpText();

        hoverInterface = FindObjectOfType<HoverUI>();
    }

    public Profession GetProfession()
    {
        return profession;
    }
    
    private void SetProfession()
    {
        int normalPersonMultiplier = 4;
        int i = UnityEngine.Random.Range(1, (System.Enum.GetValues(typeof(Profession)).Length+1)+normalPersonMultiplier-1);

        profession = Profession.NormalDude;

        if (i == 2) profession = Profession.LookOut;
        if (i == 1) profession = Profession.Sailor;

        if (tutorialHuman) profession = Profession.NormalDude;

        SetProfessionImage();
        SetProfessionBonus();
    }

    void SetStrings()
    {
        SetName();
        SetRescueSentence();
        SetProfessionBonusInfo();
        SetProfessionText();
    }

    void SetName()
    {
        String[] names = new string[] {"Tom","John","Silas","Fabian","Basti","Lukas","Jan","Adrian","Julian","Philip","Neil","Hanson","Jake","Simon"};
        int i = UnityEngine.Random.Range(0, names.Length);
        Name = names[i];
        if (profession != Profession.NormalDude) Name = Name + ",";
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
            "I saw a tornado!",
            "Damn, I got sand in my shoes",
            "I feel lost",
            "Are you real?",
            "My throat... is so dry, I... can barely... talk",
            "There was a cow staring at me, few hours ago",
            "The early bird has gold in it's mouth, hihi",
            "Did the earth stop turning?",
            "I miss the days when there were nights",
            "C-c-crazy? Me? Naaaaahaha!"};

        int i = UnityEngine.Random.Range(0, sentences.Length);
        RescueSentence = sentences[i];
    }

    void SetProfessionBonusInfo()
    {
        if (profession == Profession.Sailor) ProfessionBonusInfo = "(Speed +" + speedBonusSailor+")";
        if (profession == Profession.LookOut) ProfessionBonusInfo = "(Vision +" + visionBonusLookOut+")";
    }

    void SetProfessionText()
    {
        if (profession != Profession.NormalDude) ProfessionText = ""+profession;
        if (profession == Profession.LookOut) ProfessionText = "Look-out";
    }

    void SetPickUpText()
    {
        pickUpText.text = "\"" + RescueSentence + "\"\n- " + Name +" " + ProfessionText + " "+ProfessionBonusInfo;
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
            //pickUpTextShowingTime = 2;
            pickUpText.text = "";// Name + " " + ProfessionText + " " + ProfessionBonusInfo;
        }
        else
        {
            pickUpText.enabled = false;
        }
        //yield return new WaitForSeconds(0);
    }
}
