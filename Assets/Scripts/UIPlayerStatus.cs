using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStatus : MonoBehaviour  
{
    public RectTransform liveBar;
    public RectTransform fuelBar;
    public int barLength = 100;
    public int barHight = 100;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStatusUpdate(100, 50,100,50); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerStatusUpdate(float hitpointsMax, float hitpointsNow, float fuelMax, float fuelNow)
    {
        float hitpointsInPercent = hitpointsNow / hitpointsMax;
        float fuelInPercent = fuelNow / fuelMax;

        float lenghtLiveBarNow = barLength * hitpointsInPercent;
        float lenghtFuelBarNow = barLength * fuelInPercent;


        //liveBar.sizeDelta = new Vector2(lenghtLiveBarNow, barHight);
        fuelBar.sizeDelta = new Vector2(lenghtFuelBarNow, barHight);

    }
}
