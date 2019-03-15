using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityEnter : MonoBehaviour
{
    public AreaEffector2D wind;
    public AudioSource theme;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            wind.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            wind.enabled = true;
        }
    }
}
