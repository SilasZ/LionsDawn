using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAudio : MonoBehaviour
{
    public AudioSource sound;

    void OnCollisionEnter2D(Collision2D collision)  //Plays Sound Whenever collision detected
    {
        sound.Play();
    }
}
