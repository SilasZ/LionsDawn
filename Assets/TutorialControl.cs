using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialControl : MonoBehaviour
{
    public Text textA, TextB, textC1,textC2, textD1,textD2;
    bool clickOnWood = false;
    bool showStatueText = false;
    public GameObject tutorialWood;
    GameObject player;
    public LionStatues statues;
    // Start is called before the first frame update
    void Start()
    {
        textA.enabled = true;
        player = FindObjectOfType<Movement>().gameObject;
    }



    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey("w")|| Input.GetKey("a")|| Input.GetKey("s")|| Input.GetKey("d"))
        {
            clickOnWood = true;
            textA.enabled = false;
        }

        if (clickOnWood&&tutorialWood)
        {
            TextB.enabled = true;
        }

        if (!tutorialWood&&!showStatueText)
        {
            TextB.enabled = false;
            textC1.enabled = true;
            textC2.enabled = true;
        }
        if (showStatueText)
        {
            textC1.enabled = false;
            textC2.enabled = false;
            textD1.enabled = true;
            textD2.enabled = true;
        }

        if (statues.GotClickedFirstTime())
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        showStatueText = true;
    }
}
