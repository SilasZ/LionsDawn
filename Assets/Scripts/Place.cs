using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour
{

    public void AddPerson(PersonMovement person)
    {       
        person.transform.position = transform.position - Vector3.forward;
        person.transform.parent = transform;
    }
}
