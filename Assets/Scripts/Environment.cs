using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public int numObstacles;
    public float mapSizeX;
    public float mapSizeY;
    public float objectScaleFactor;
    public GameObject[] prefabsObstacle;
    public float spawnRadius;
    public float perlinScale;

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        while (i < numObstacles)
        {
            float xPos = Random.Range(-mapSizeX / 2, mapSizeX / 2);
            float yPos = Random.Range(-mapSizeY / 2, mapSizeY / 2);
            if (Mathf.Pow(xPos, 2) + Mathf.Pow(yPos, 2) < spawnRadius)
            {
                continue;
            }

            float noiseValue = Mathf.PerlinNoise(xPos / perlinScale, yPos / perlinScale);
            if (noiseValue > 0.4)
            {
                int index = Random.Range(0, prefabsObstacle.Length);
                GameObject prefabObject = prefabsObstacle[index];
                Quaternion orientation = Quaternion.AngleAxis(Random.Range(0f, 360f), new Vector3(0, 0, 1));
                Transform tf = Instantiate(prefabObject, new Vector3(xPos, yPos, 0), orientation).transform;
                tf.localScale = new Vector3(objectScaleFactor * noiseValue, objectScaleFactor * noiseValue, 1);
                i++;
            }
        }
    }
}
