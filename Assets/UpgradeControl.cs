using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeControl : MonoBehaviour

    
{
    public Movement playerMovement;
    public Text WaterVesselCostText;

    int WaterVesselUpgradeCost = 3;
    int WaterVesselUpgradeNumber = 0;

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
        if ((playerMovement.HasEnoughWood(WaterVesselUpgradeCost)) && (WaterVesselUpgradeNumber < 3))
        {
            playerMovement.AddWood(-WaterVesselUpgradeCost);
            playerMovement.IncreaseWaterMaxBy(40);

            WaterVesselUpgradeCost = WaterVesselUpgradeCost + 3;
            RefreshCostTexts();
            WaterVesselUpgradeNumber++;
        }
        

    }

    void RefreshCostTexts()
    {
        WaterVesselCostText.text = "" + WaterVesselUpgradeCost;
    }
}
