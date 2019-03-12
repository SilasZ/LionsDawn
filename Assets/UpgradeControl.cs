using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeControl : MonoBehaviour

    
{
    public Movement playerMovement;
    public Sprite checkboxFilled;

    public Text WaterVesselCostText;
    public Image WaterCostSymbol;
    public Image waterCheckbox1;
    public Image waterCheckbox2;
    public Image waterCheckbox3;
    public Button waterUpgradeButton;

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
            playerMovement.IncreaseWaterMaxBy(50);
            

            WaterVesselUpgradeCost = WaterVesselUpgradeCost + 2;
            RefreshCostTexts();
            if (WaterVesselUpgradeNumber==0) waterCheckbox1.sprite = checkboxFilled; ;
            if (WaterVesselUpgradeNumber == 1) waterCheckbox2.sprite = checkboxFilled; ;
            if (WaterVesselUpgradeNumber == 2)
            {
                waterCheckbox3.sprite = checkboxFilled; ;
                WaterVesselCostText.text = "";
                WaterCostSymbol.enabled = false;
                waterUpgradeButton.gameObject.SetActive(false);
            }

            

            WaterVesselUpgradeNumber++;

        }
        

    }

    void RefreshCostTexts()
    {
        WaterVesselCostText.text = "" + WaterVesselUpgradeCost;
    }
}
