using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public int numObstacles;
    public float mapSizeX;
    public float mapSizeY;
    public GameObject[] prefabsObstacle;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<numObstacles; i++)
        {
            float xPos = Random.Range(-mapSizeX / 2, mapSizeX / 2);
            float yPos = Random.Range(-mapSizeY / 2, mapSizeY / 2);
            float noiseValue = Mathf.PerlinNoise(xPos, yPos);
            if (noiseValue > 0.3)
            {
                int index = Random.Range(0, prefabsObstacle.Length);
                GameObject prefabObject = prefabsObstacle[index];
                Quaternion orientation = Quaternion.AngleAxis(Random.Range(0f, 360f), new Vector3(0, 0, 1));
                Transform tf = Instantiate(prefabObject, new Vector3(xPos, yPos, 0), orientation).transform;
                tf.localScale.Set(noiseValue, noiseValue, 1);
            }
        }
    }
}
