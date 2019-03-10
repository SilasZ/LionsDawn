using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionStatues : MonoBehaviour
{

    public Movement movement;

    public UIPlayerStatus playerUI;

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
        endDay();
    }

    void endDay()
    {
        movement.RefillWaterTank();
        playerUI.newDay();
    }
}
