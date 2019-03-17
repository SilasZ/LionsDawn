using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverUI : MonoBehaviour
{
    public Text hoverText;
    public Image background;

    public void hide()
    {
        hoverText.enabled = false;
        background.enabled = false;
    }

    public void show()
    {
        hoverText.enabled = true;
        background.enabled = true;
    }
}
