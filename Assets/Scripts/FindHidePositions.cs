using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindHidePositions : MonoBehaviour
{
    public Transform tf;
    public Vector2 referencePos;
    const int listLength = 30;
    Vector2[] oldPositions = new Vector2[listLength];
    int i = 0;
    public GameObject rn;
    // Start is called before the first frame update
    void Start()
    {

    }

    bool ValidatePosition()
    {
        Collider2D coll = Physics2D.OverlapCircle(tf.position, 1);  
        return !(bool) coll;
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
        if ((i > listLength) && ((Vector2) tf.position - oldPositions[i%listLength]).magnitude < .2)
        {
            GameObject r = Instantiate(rn, tf.position, Quaternion.identity);
            r.GetComponent<FindHidePositions>().referencePos = (Vector2)tf.position;
            this.enabled = false;
        }
        oldPositions[i%listLength] = tf.position;
        i++;
        MCStep();
    }
}
