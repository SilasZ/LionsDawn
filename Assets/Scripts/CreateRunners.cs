using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRunners : MonoBehaviour
{
    int i = 0;
    public GameObject rn;
    public int numRunners;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while (i < numRunners)
        {
            Vector3 v3 = new Vector3(Random.Range(-10f, 10f), Random.Range(-20f, 20f), 0);
            Instantiate(rn, v3, Quaternion.identity);
            i++;
        }
    }
}
