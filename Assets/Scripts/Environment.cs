using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public int numObstacles;
    public float mapSizeR;
    public float objectScaleFactor;
    public GameObject[] prefabsObstacle;
    public float spawnRadius;
    public float perlinScale;
    public float dAngle;
    public GameObject runnerSpawn;

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

            if (r > (float) 3 / 4 * mapSizeR)
            {
                noiseValue += (4 * r / mapSizeR - 3) / 5;
                noiseValue = Mathf.Clamp(noiseValue, 0, 1);
            }

            if (noiseValue > 0.4)
            {
                int index = Random.Range(0, prefabsObstacle.Length);
                GameObject prefabObject = prefabsObstacle[index];
                Quaternion orientation = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward);
                Transform tf = Instantiate(prefabObject, new Vector3(xPos, yPos, -2), orientation).transform;
                tf.localScale = new Vector3(objectScaleFactor * noiseValue, objectScaleFactor * noiseValue, 1);
                i++;
            }
        }

        angle = 0;
        while (angle < 2 * Mathf.PI)
        {
            float xPos = Mathf.Cos(angle) * mapSizeR;
            float yPos = Mathf.Sin(angle) * mapSizeR;
            int index = Random.Range(0, prefabsObstacle.Length);
            GameObject prefabObject = prefabsObstacle[index];
            Quaternion orientation = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward);
            Transform tf = Instantiate(prefabObject, new Vector3(xPos, yPos, -2), orientation).transform;
            tf.localScale = new Vector3(objectScaleFactor, objectScaleFactor, 1);
            angle += dAngle;
        }
        runnerSpawn.GetComponent<CreateRunners>().Create();
        Invoke("Decorate", 1); //Dirty Hack. Do not try this at home!
    }

    private void Decorate()
    {
        foreach (var deco in FindObjectsOfType<Decoration>()) deco.Decorate();
    }

}
