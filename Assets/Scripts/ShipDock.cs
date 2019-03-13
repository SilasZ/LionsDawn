using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShipDock : MonoBehaviour

{
    public GameObject upgradeInterface;
    public GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Player.transform.position - transform.position).magnitude > 20)
        {
            if (upgradeInterface.activeSelf)
            {
                upgradeInterface.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Movement>())
        {
            upgradeInterface.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Movement>())
        {
            upgradeInterface.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (upgradeInterface.activeSelf)
            {
                upgradeInterface.SetActive(false);
            }
            else
            {
                upgradeInterface.SetActive(true);
            }
        } 
    }
}
