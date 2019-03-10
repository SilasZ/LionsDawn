using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindHidePositions : MonoBehaviour
{
    public Transform tf;
    public Vector2 referencePos;
    public int arrLength;
    Vector2[] oldPositions;
    int i = 0;
    public GameObject rn;
    public GameObject tent;
    // Start is called before the first frame update
    void Start()
    {
        oldPositions = new Vector2[arrLength];
    }

    bool ValidatePosition()
    {
        Collider2D coll = Physics2D.OverlapCircle(tf.position, 1, 1);
        Debug.Log((bool)coll);
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
        if ((i > arrLength) && ((Vector2) tf.position - oldPositions[i%arrLength]).magnitude < .2)
        {
            GameObject r = Instantiate(rn, tf.position, Quaternion.identity);
            r.GetComponent<FindHidePositions>().referencePos = (Vector2)tf.position;
            Instantiate(tent, tf.position, Quaternion.AngleAxis(Random.Range(0f, 360f), new Vector3(0, 0, 1)));
            Destroy(this.gameObject);
        }
        oldPositions[i%arrLength] = tf.position;
        i++;
        MCStep();
    }
}
