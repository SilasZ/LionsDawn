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
    Vector3 goalVel;

    Vector3 dVel;
    // Start is called before the first frame update
    void Start()
    {
        vel = new Vector3(0, 0.1f, 0);
        goalVel = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward) * Vector3.up * Random.Range(minVel, maxVel);
    }

    void FixedUpdate()
    {
        if ((vel - goalVel).magnitude < 0.001)
        {
            goalVel = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward) * Vector3.up * Random.Range(minVel, maxVel);
        }
        transform.position += vel;
        if (transform.position.magnitude < minR)
        {
            dVel = transform.position;
            dVel[2] = 0;
            goalVel += 0.01f * dVel.normalized;
            if (goalVel.magnitude > maxVel)
            {
                goalVel *= maxVel / goalVel.magnitude;
            }
        }

        if (transform.position.magnitude > maxR)
        {
            dVel = transform.position;
            dVel[2] = 0;
            goalVel -= 0.01f * dVel.normalized;
            if (goalVel.magnitude > maxVel)
            {
                goalVel *= maxVel / goalVel.magnitude;
            }
        }
        vel = 0.99f * vel + 0.01f * goalVel;
    }
}
