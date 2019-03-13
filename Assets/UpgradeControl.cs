using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeControl : MonoBehaviour

    
{
    public Movement playerMovement;
    public Sprite checkboxFilled;


    //water upgrade-----------------------------------------------
    public Text WaterVesselCostText;
    public Image WaterCostSymbol;
    public Image waterCheckbox1;
    public Image waterCheckbox2;
    public Image waterCheckbox3;
    public Button waterUpgradeButton;

    public int WaterCost = 3;
    public int WaterCostIncrease = 2;
    public float WaterGainPerLevel = 50;

    int WaterVesselUpgradeNumber = 0;
    //-----------------------------------------------------------



    //hull upgrade-----------------------------------------------
    public Text hullCostText;
    public Image hullCostSymbol;
    public Image hullCheckbox1;
    public Image hullCheckbox2;
    public Image hullCheckbox3;
    public Button hullUpgradeButton;
    
    public int hullCost = 2;
    public int hullCostIncrease = 2;
    public float hullGainPerLevel = 50;

    int hullUpgradeNumber = 0;
    //-----------------------------------------------------------


        
    //crew upgrade-----------------------------------------------
    public Text crewCostText;
    public Image crewCostSymbol;
    public Image crewCheckbox1;
    public Image crewCheckbox2;
    public Image crewCheckbox3;
    public Image crewCheckbox4;
    public Image crewCheckbox5;
    public Button crewUpgradeButton;

    public int crewCost = 3;
    public int crewCostIncrease = 1;
    public int crewGainPerLevel = 1;

    int crewUpgradeNumber = 0;
    //-----------------------------------------------------------



    //crows nest ------------------------------------------------
    public Text nestCostText;
    public Image nestCostSymbol;
    public Button nestUpgradeButton;
    public GameObject nestBuiltSymbol;

    public int nestCost = 12;
    //-----------------------------------------------------------



    //repair nest ------------------------------------------------
    public Text repairCostText;

    public int repairCost = 1;
    //-----------------------------------------------------------

    public Color sufficient, insufficient;

    // Start is called before the first frame update
    void Start()
    {
        RefreshCostTexts();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf) refreshCostColor();
    }

    private void refreshCostColor()
    {
        //water
        if (playerMovement.HasEnoughWood(WaterCost))
        {
            WaterVesselCostText.color = sufficient;
        }
        else
        {
            WaterVesselCostText.color = insufficient;
        }

        //hull
        if (playerMovement.HasEnoughWood(hullCost))
        {
            hullCostText.color = sufficient;
        }
        else
        {
            hullCostText.color = insufficient;
        }

        //crew
        if (playerMovement.HasEnoughWood(crewCost))
        {
            crewCostText.color = sufficient;
        }
        else
        {
            crewCostText.color = insufficient;
        }

        //nest
        if (playerMovement.HasEnoughWood(nestCost))
        {
            nestCostText.color = sufficient;
        }
        else
        {
            nestCostText.color = insufficient;
        }

        //repair
        if (playerMovement.HasEnoughWood(repairCost))
        {
            repairCostText.color = sufficient;
        }
        else
        {
            repairCostText.color = insufficient;
        }
    }

    public void UpgradeWaterVessel()
    {
        if ((playerMovement.HasEnoughWood(WaterCost)) && (WaterVesselUpgradeNumber < 3))
        {
            playerMovement.AddWood(-WaterCost);
            playerMovement.IncreaseWaterMaxBy(WaterGainPerLevel);
            
            WaterCost = WaterCost + WaterCostIncrease;
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

    public void UpgradeHull()
    {
        if ((playerMovement.HasEnoughWood(hullCost)) && (hullUpgradeNumber < 3))
        {
            playerMovement.AddWood(-hullCost);
            playerMovement.IncreaseHullMaxBy(hullGainPerLevel);


            hullCost = hullCost + hullCostIncrease;
            RefreshCostTexts();
            if (hullUpgradeNumber == 0) hullCheckbox1.sprite = checkboxFilled; ;
            if (hullUpgradeNumber == 1) hullCheckbox2.sprite = checkboxFilled; ;
            if (hullUpgradeNumber == 2)
            {
                hullCheckbox3.sprite = checkboxFilled; ;
                hullCostText.text = "";
                hullCostSymbol.enabled = false;
                hullUpgradeButton.gameObject.SetActive(false);
            }
            hullUpgradeNumber++;
        }
    }

    public void UpgradeCrew() 
    {
        if ((playerMovement.HasEnoughWood(crewCost)) && (crewUpgradeNumber < 5))
        {
            playerMovement.AddWood(-crewCost);
            playerMovement.IncreaseCrewMaxBy(crewGainPerLevel);

            
            crewCost = crewCost + crewCostIncrease;
            RefreshCostTexts();
            if (crewUpgradeNumber == 0) crewCheckbox1.sprite = checkboxFilled; ;
            if (crewUpgradeNumber == 1) crewCheckbox2.sprite = checkboxFilled; ;
            if (crewUpgradeNumber == 2) crewCheckbox3.sprite = checkboxFilled; ;
            if (crewUpgradeNumber == 3) crewCheckbox4.sprite = checkboxFilled; ;
            if (crewUpgradeNumber == 4)
            {
                crewCheckbox5.sprite = checkboxFilled; ;
                crewCostText.text = "";
                crewCostSymbol.enabled = false;
                crewUpgradeButton.gameObject.SetActive(false);
            }
            crewUpgradeNumber++;
        }
    }

    public void UpgradeGetCrowsNest()
    {
        if (playerMovement.HasEnoughWood(nestCost))
        {
            playerMovement.AddWood(-nestCost);
            playerMovement.AddCrowsNest();
            RefreshCostTexts();

            nestCostText.text = "";
            nestCostSymbol.enabled = false;
            nestUpgradeButton.gameObject.SetActive(false);

            nestBuiltSymbol.SetActive(true);
        }
    }

    public void RepairShip()
    {
        if (playerMovement.HasEnoughWood(repairCost))
        {
            playerMovement.AddWood(-repairCost);
            playerMovement.RepairShipComplete();
            RefreshCostTexts();
        }
    }

    void RefreshCostTexts()
    {
        if (waterUpgradeButton.gameObject.activeSelf)   WaterVesselCostText.text = "" + WaterCost;
        if (hullUpgradeButton.gameObject.activeSelf)    hullCostText.text = "" + hullCost;
        if (crewUpgradeButton.gameObject.activeSelf)    crewCostText.text = "" + crewCost;
        if (nestUpgradeButton.gameObject.activeSelf)    nestCostText.text = "" + nestCost;

        repairCostText.text = "" + repairCost;
    }
}
