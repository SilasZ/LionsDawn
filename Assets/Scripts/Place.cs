using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour
{
    public int id;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPerson(PersonMovement person)
    {       
        person.transform.position = transform.position;
        person.transform.parent = transform;
    }
}
