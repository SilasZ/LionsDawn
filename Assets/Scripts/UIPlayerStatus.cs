using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerStatus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerStatusUpdate(float hitpointsMax, float hitpointsNow)
    {
        float hitpointsInPercent = hitpointsNow / hitpointsMax;

        for (float f = hitpointsInPercent; f > 0; f=f-0.1F)
        {
            //Stuff
        }
    }
}
