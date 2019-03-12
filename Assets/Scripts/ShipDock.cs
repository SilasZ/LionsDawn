using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShipDock : MonoBehaviour

{
    bool uiActive = false;
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
            if (uiActive)
            {
                uiActive = false;
                upgradeInterface.SetActive(false);
            }
        }
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (uiActive)
            {
                uiActive = false;
                upgradeInterface.SetActive(false);
            }
            else
            {
                uiActive = true;
                upgradeInterface.SetActive(true);
            }
        } 
    }
}
