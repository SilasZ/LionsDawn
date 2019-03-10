using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PersonMovement : MonoBehaviour
{

    public float beamRadius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Beam(); 
    }

    private void Beam()
    {
        var places = FindObjectsOfType<Place>().Where(a => !transform.parent || a.transform.parent != transform.parent.parent);
        var nearestPlace = places.OrderBy(a => Vector2.Distance(a.transform.position, transform.position)).FirstOrDefault();   
        if (nearestPlace) nearestPlace.GetComponent<Place>().AddPerson(this);

    }
}
