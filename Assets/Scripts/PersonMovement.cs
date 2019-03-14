using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    float speedBonus = 0;
    float visionBonus = 0;
    

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
        if (profession == Profession.sailor) speedBonus = 5;
        if (profession == Profession.lookOut) visionBonus = 2;
    }

    public float GetSpeedBonus()
    {
        return speedBonus;
    }
    public float GetVisionBonus()
    {
        return visionBonus;
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
        }      
    }
}
