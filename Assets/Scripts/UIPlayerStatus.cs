using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPlayerStatus : MonoBehaviour  
{
    public RectTransform liveBar;
    public RectTransform fuelBar;
    public Text deadText;
    public Text startText;
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

    private void Start()
    {
        StartCoroutine(StartGame());
    }

    public void ShowDeadText()
    {
        deadText.enabled=true;

        StartCoroutine(EndGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(4);
        startText.enabled = false;
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(4);
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

}
