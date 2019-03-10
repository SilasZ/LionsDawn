using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PersonMovement : MonoBehaviour
{

    public float beamRadius;
    public GameObject beam;
    private float maxDistance = 10;

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
        }      
    }
}
