using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour
{
    public int numObstacles;
    public float mapSizeR;
    public float objectScaleFactor;
    public GameObject[] prefabsObstacle;
    public float spawnRadius;
    public float perlinScale;
    public float threshold;
    public int z;

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        float angle;

        while (i < numObstacles)
        {
            float r = Random.Range(Mathf.Pow(spawnRadius, 3), Mathf.Pow(mapSizeR, 3));
            r = Mathf.Pow(r, (float)1 / 3);
            angle = Random.Range(0, 2 * Mathf.PI);
            float xPos = Mathf.Cos(angle) * r;
            float yPos = Mathf.Sin(angle) * r;


            float noiseValue = Mathf.PerlinNoise(xPos / perlinScale, yPos / perlinScale);

            if (noiseValue > threshold)
            {
                int index = Random.Range(0, prefabsObstacle.Length);
                GameObject prefabObject = prefabsObstacle[index];
                Quaternion orientation = Quaternion.AngleAxis(Random.Range(0f, 360f), new Vector3(0, 0, 1));
                Transform tf = Instantiate(prefabObject, new Vector3(xPos, yPos, z), orientation).transform;
                tf.localScale = new Vector3(objectScaleFactor, objectScaleFactor, 1);
                i++;
            }
        }
    }
}
