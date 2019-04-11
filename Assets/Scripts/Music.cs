using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    Wind wind;
    AudioSource audioSource;
    public bool inCity;
    float dVol = 0.002f;
    float desiredVolume = 0;
    // Start is called before the first frame update
    void Start()
    {
        wind = FindObjectOfType<Wind>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (inCity)
        {
            desiredVolume = 0;
        } else
        {
            desiredVolume = Mathf.Clamp(1 - (wind.strength / wind.maxStrength)*1.5f, 0, 1);
        }
        audioSource.volume += Mathf.Sign(desiredVolume - audioSource.volume) * dVol;
    }
}
