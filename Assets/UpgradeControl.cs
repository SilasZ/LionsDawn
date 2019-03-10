using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeControl : MonoBehaviour

    
{
    public Movement playerMovement;
    public Text WaterVesselCostText;

    int WaterVesselUpgradeCost = 3;

    // Start is called before the first frame update
    void Start()
    {
        RefreshCostTexts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeWaterVessel()
    {
        playerMovement.AddWood(-WaterVesselUpgradeCost);
        WaterVesselUpgradeCost = WaterVesselUpgradeCost + 3;
        RefreshCostTexts();

    }

    void RefreshCostTexts()
    {
        WaterVesselCostText.text = "" + WaterVesselUpgradeCost;
    }
}
