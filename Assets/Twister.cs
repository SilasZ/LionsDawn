using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twister : MonoBehaviour
{
    Movement player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (((Vector2) (other.GetComponent<Transform>().position - transform.position)).magnitude < 30)
            {
                other.GetComponentInParent<Rigidbody2D>().AddTorque(3);
                player.DealDamage(.2f);
            }
        }
    }
}
