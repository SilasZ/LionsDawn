using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    Wind wind;
    AudioSource audioSource;
    public bool inCity;
    bool fading;
    float dVol = 0.002f;
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
            audioSource.volume = Mathf.Clamp(audioSource.volume - dVol, 0, 1);
            fading = true;
        } else
        {
            if (fading)
            {
                audioSource.volume += Mathf.Sign(1 - (wind.strength / wind.maxStrength) - audioSource.volume) * dVol;
                if (Mathf.Abs(1 - (wind.strength / wind.maxStrength) - audioSource.volume) < dVol)
                {
                    fading = false;
                }
            }
            else
            {
                audioSource.volume = 1 - (wind.strength / wind.maxStrength);
            }
        }
    }
}
