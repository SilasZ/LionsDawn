using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRunners : MonoBehaviour
{
    int i = 0;
    public GameObject rn;
    public int numRunners;
    public int numDescendingGenerations;
    public int spawnRadius;
    // Start is called before the first frame update
    void Start()
    {
        float angle = 0;
        float dAngle = 2 * Mathf.PI / numRunners;
        while (i < numRunners)
        {
            Vector3 v3 = new Vector3(spawnRadius * Mathf.Cos(angle), spawnRadius * Mathf.Sin(angle), 0);
            GameObject runner = Instantiate(rn, v3, Quaternion.identity);
            runner.GetComponent<FindHidePositions>().followingGenerations = numDescendingGenerations;
            angle += dAngle;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
