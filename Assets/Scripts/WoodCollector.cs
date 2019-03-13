using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCollector : MonoBehaviour
{
    public GameObject beam;
    Movement player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Instantiate(beam, transform.position - Vector3.forward, Quaternion.identity);
        player.AddWood(1);
        Destroy(this.gameObject);
    }
}
