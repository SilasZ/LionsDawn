using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    Wind wind;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        wind = FindObjectOfType<Wind>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = 1 - (wind.strength / wind.maxStrength);
    }
}
