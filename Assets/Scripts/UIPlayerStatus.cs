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

    public Text woodCount;

    int day = 1;

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

    public void newDay()
    {
        day++;
        startText.enabled = true;
        startText.text = "Day " + day;
        StartCoroutine(HideTextAfterTime(startText,2));
    }

    public void ShowDeadText()
    {
        deadText.enabled=true;

        StartCoroutine(EndGame());
    }

    public void NewWoodCount(int count)
    {
        woodCount.text =""+ count;
    }

    IEnumerator HideTextAfterTime(Text text, int secounds)
    {
        yield return new WaitForSeconds(secounds);
        text.enabled = false;
    }


    IEnumerator StartGame()
    {
        startText.enabled = true;
        yield return new WaitForSeconds(4);
        startText.enabled = false;
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(4);
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

}
