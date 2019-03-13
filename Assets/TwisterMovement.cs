using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwisterMovement : MonoBehaviour
{

    Vector3 vel;
    public float minVel;
    public float maxVel;
    public float minR;
    public float maxR;
    float goalVel;

    Vector3 dVel;
    // Start is called before the first frame update
    void Start()
    {
        vel = new Vector3(0, 0.1f, 0);
        goalVel = Random.Range(minVel, maxVel);
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(vel.magnitude - goalVel) < 0.001)
        {
            goalVel = Random.Range(minVel, maxVel);
        }
        transform.position += vel;
        if (transform.position.magnitude < minR)
        {
            dVel = transform.position;
            dVel[2] = 0;
            vel += 0.01f * dVel.normalized;
        }

        if (transform.position.magnitude > maxR)
        {
            dVel = transform.position;
            dVel[2] = 0;
            vel -= 0.01f * dVel.normalized;
        }
        vel *= 0.99f + 0.01f * goalVel / vel.magnitude;
    }
}
