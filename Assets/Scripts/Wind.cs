using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    AreaEffector2D area;
    ParticleSystemForceField particleForce;
    AudioSource audioSource;
    public float strength = 0;
    public float maxStrength = 5;

    // Start is called before the first frame update
    void Start()
    {
        area = GetComponent<AreaEffector2D>();
        particleForce = GetComponent<ParticleSystemForceField>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        strength += (Random.value - 0.5f) * 0.1f;
        strength = Mathf.Max(0.1f, Mathf.Min(maxStrength, strength));
        transform.Rotate(new Vector3(0, 0, (Random.value - 0.5f) / strength));
        area.forceMagnitude = strength;
        particleForce.directionX = strength * 10f / transform.localScale.x;
        audioSource.volume = strength / maxStrength;
    }
}
