using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDock : MonoBehaviour

{
    bool uiActive = false;
    public GameObject upgradeInterface;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
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
