using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Bars {life, water};

public class UIPlayerStatus : MonoBehaviour  
{

    //Zeilenenden geändert auf windows CR LF
    public int barStartLength = 300;
    int barHight = 30;
    public RectTransform liveBar;
    public RectTransform liveFrame;
    public RectTransform fuelBar;
    public RectTransform fuelFrame;
    public Text deadText;
    public Text startText;
    public Text crewCountText;
    public GameObject crewBonusWindow;
    public Text crewBonustext;

    int lifeBarLength;
    int lifeFrameLength;
    int fuelBarLength;
    int fuelFrameLength;

    public Text woodCount;

    int sleptHours = 1;
    int hours = 0;

    public void IncreaseBarByPercentOfStartValue(Bars bar,float percent)
    {
        float multiplier = percent / 100;
        float increase = barStartLength * multiplier;

        if(bar == Bars.life)
        {
            lifeBarLength = lifeBarLength + (int)increase;
            lifeFrameLength = lifeFrameLength + (int)increase;
            liveFrame.sizeDelta = new Vector2(lifeFrameLength, barHight+10);
        }
        if(bar == Bars.water)
        {
            fuelBarLength = fuelBarLength + (int)increase;
            fuelFrameLength = fuelFrameLength + (int)increase;
            fuelFrame.sizeDelta = new Vector2(fuelFrameLength, barHight+10);
        }  
    }

    public void PlayerStatusUpdate(float hitpointsMax, float hitpointsNow, float fuelMax, float fuelNow)
    {
        float hitpointsInPercent = hitpointsNow / hitpointsMax;
        float fuelInPercent = fuelNow / fuelMax;

        float lenghtLiveBarNow = lifeBarLength * hitpointsInPercent;
        float lenghtFuelBarNow = fuelBarLength * fuelInPercent;


        liveBar.sizeDelta = new Vector2(lenghtLiveBarNow, barHight);
        fuelBar.sizeDelta = new Vector2(lenghtFuelBarNow, barHight);

    }

    private void Start()
    {
        setStartBarSizes();
    }

    void setStartBarSizes()
    {
        lifeBarLength = barStartLength - 5;
        lifeFrameLength = barStartLength;
        fuelBarLength = barStartLength - 5;
        fuelFrameLength = barStartLength;
    }

    public void newDay(bool gotClickedBefore)
    {
        

        sleptHours++;

        hours = ((int) Time.time/60)+ sleptHours;
        Debug.Log("hours:" + hours);

        startText.enabled = true;
        startText.text ="You first launched "+hours+" hours ago.";
        int i = 2;
        if (!gotClickedBefore)
        {
            startText.text = startText.text + "\n People won't make it forever...";
            i = 5;
        }

        FindObjectOfType<humansList>().KillPeople(hours);

        StartCoroutine(HideTextAfterTime(startText,i));
    }

    public void NewCrewCount(int crewNow, int crewMax)
    {
        crewCountText.text = crewNow + "/" + crewMax;
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

    public void RefreshCrewBonus(float speedBonus, float visionBonus)
    {
        if (speedBonus > 0 || visionBonus > 0)
        {
            crewBonusWindow.SetActive(true);
            string speedpart = "";
            if(speedBonus>0) speedpart= "\nSpeed +"+speedBonus;
            string visionpart = "";
            if (visionBonus > 0) visionpart = "\nVision +" + visionBonus;
            crewBonustext.text = "Crew bonus\n" + speedpart + visionpart;
        }
        else
        {
            crewBonusWindow.SetActive(false);
        }
    }

    IEnumerator HideTextAfterTime(Text text, int secounds)
    {
        yield return new WaitForSeconds(secounds);
        text.enabled = false;
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(4);
        //Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        SceneManager.LoadScene("GameLoader", LoadSceneMode.Single);
    }

    public void GameFinished(int hoursItTookYou)
    {
        int savedSouls = 0;
        foreach(PersonMovement p in FindObjectsOfType<PersonMovement>())
        {
            if (p.GetComponentInParent<Place>()) savedSouls++;
        }
        deadText.color = new Color(255, 255, 255);
        deadText.text="The sun burned this land\nfor more than "+hoursItTookYou+" hours now.\nNoone out there can still be alive...\n You saved "+savedSouls+" lost souls.\nWell done!";
        deadText.enabled = true;
        startText.enabled = false;
        StartCoroutine(CloseGame());
    }

    IEnumerator CloseGame()
    {
        yield return new WaitForSeconds(8);
        Application.Quit();
    }

}
