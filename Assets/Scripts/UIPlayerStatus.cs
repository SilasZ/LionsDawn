﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStatus : MonoBehaviour  
{
    public RectTransform liveBar;
    public RectTransform fuelBar;
    int barLength = 100;
    int barHight = 100;

    public void PlayerStatusUpdate(float hitpointsMax, float hitpointsNow, float fuelMax, float fuelNow)
    {
        float hitpointsInPercent = hitpointsNow / hitpointsMax;
        float fuelInPercent = fuelNow / fuelMax;

        float lenghtLiveBarNow = barLength * hitpointsInPercent;
        float lenghtFuelBarNow = barLength * fuelInPercent;


        liveBar.sizeDelta = new Vector2(lenghtLiveBarNow, barHight);
        fuelBar.sizeDelta = new Vector2(lenghtFuelBarNow, barHight);

    }
}
