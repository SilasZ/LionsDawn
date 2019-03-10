using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindHidePositions : MonoBehaviour
{
    public Transform tf;
    public Vector2 referencePos;
    public int arrLength;
    Vector2[] oldPositions;
    public int i = 0;
    public GameObject rn;
    public GameObject[] tents;
    public GameObject human;
    public int followingGenerations;
    public float minDist;
    // Start is called before the first frame update
    void Start()
    {
        oldPositions = new Vector2[arrLength];
        while (true)
        {
            if ((i > arrLength) && ((Vector2)tf.position - oldPositions[i % arrLength]).magnitude < .2)
            {
                GameObject[] allTents = GameObject.FindGameObjectsWithTag("Tent");
                bool breaking = false;
                foreach (GameObject placedTent in allTents)
                {
                    if (((Vector2)(placedTent.transform.position - tf.position)).magnitude < minDist)
                    {
                        this.GetComponent<FindHidePositions>().referencePos = tf.position;
                        this.GetComponent<FindHidePositions>().i = 0;
                        breaking = true;
                        break;
                    }
                }
                if (breaking)
                {
                    continue;
                }

                if (followingGenerations > 0)
                {
                    GameObject r = Instantiate(rn, tf.position, Quaternion.identity);
                    r.GetComponent<FindHidePositions>().referencePos = (Vector2)tf.position;
                    r.GetComponent<FindHidePositions>().followingGenerations = followingGenerations - 1;
                }
                GameObject tent = tents[Random.Range(0, tents.Length)];
                GameObject newTent = Instantiate(tent, tf.position, Quaternion.AngleAxis(Random.Range(0f, 360f), new Vector3(0, 0, 1)));
                newTent.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                float angle = Random.Range(0f, 2 * Mathf.PI);
                Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
                Instantiate(human, tf.position + offset, Quaternion.AngleAxis(Random.Range(0f, 360f), new Vector3(0, 0, 1)));
                Destroy(this.gameObject);
                return;
            }
            oldPositions[i % arrLength] = tf.position;
            i++;
            MCStep();
        }
    }

    bool ValidatePosition()
    {
        Collider2D coll = Physics2D.OverlapCircle(tf.position, 1, 1);
        return !(bool)coll;
    }

    void MCStep()
    {
        float angle = Random.Range(0f, 360f);
        Vector2 direction1 = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
        Vector2 direction2 = ((Vector2) (tf.position) - referencePos).normalized;
        Vector3 oldPosition = tf.position;
        tf.position += (Vector3) (direction1 + direction2 / 4);
        if (!ValidatePosition())
        {
            tf.position = oldPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
