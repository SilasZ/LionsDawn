using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialControl : MonoBehaviour
{
    public Text textA, TextB, textC1,textC2, textD1,textD2, textD3, textD4, textD5, textD6, textE1, textE2, textE3, textE4, textF1, textF2, textEND;
    bool clickOnWood = false;
    bool personClicking = false;
    bool putPersonInTown = false;
    public GameObject tutorialWood;
    public PersonMovement tutorialHuman;
    public GameObject tutorialTent;
    GameObject player;
    public LionStatues statues;
    public GameObject skipButton;

    bool endingTutorial = false;
    // Start is called before the first frame update
    void Start()
    {
        textA.enabled = true;
        skipButton.SetActive(true);
        player = FindObjectOfType<Movement>().gameObject;
        tutorialHuman.gameObject.SetActive(false);
        tutorialTent.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey("w")|| Input.GetKey("a")|| Input.GetKey("s")|| Input.GetKey("d"))
        {
            clickOnWood = true;
            textA.enabled = false;
            skipButton.SetActive(false);
        }

        if (clickOnWood&&tutorialWood)
        {
            TextB.enabled = true;
        }

        if (!tutorialWood&&!personClicking)
        {
            TextB.enabled = false;
            textC1.enabled = true;
            textC2.enabled = true;

            
        }

        if (personClicking&&!tutorialHuman.GetComponentInParent<Movement>()&& !tutorialHuman.GetComponentInParent<Place>())
        {
            textC1.enabled = false;
            textC2.enabled = false;
            textD1.enabled = true;
            textD2.enabled = true;
            textD3.enabled = true;
            textD4.enabled = true;
            textD5.enabled = true;
            textD6.enabled = true;

            tutorialTent.SetActive(true);
            tutorialHuman.gameObject.SetActive(true);

        }

        if (tutorialHuman.GetComponentInParent<Movement>()&&!putPersonInTown)
        {
            putPersonInTown = true;
            textD1.enabled = false;
            textD2.enabled = false;
            textD3.enabled = false;
            textD4.enabled = false;
            textD5.enabled = false;
            textD6.enabled = false;
            textE1.enabled = true;
            textE2.enabled = true;
            textE3.enabled = true;
            textE4.enabled = true;
        }

        if(tutorialHuman.GetComponentInParent<Place>()&& !tutorialHuman.GetComponentInParent<Movement>())
        {
            textD1.enabled = false;
            textD2.enabled = false;
            textD3.enabled = false;
            textD4.enabled = false;
            textD5.enabled = false;
            textD6.enabled = false;
            textE1.enabled = false;
            textE2.enabled = false;
            textE3.enabled = false;
            textE4.enabled = false;
            Destroy(tutorialTent);

            textF1.enabled = true;
            textF2.enabled = true;
        }


        if (statues.GotClickedFirstTime()&&!endingTutorial)
        {
            endingTutorial = true;
            StartCoroutine(EndTutorial());
        }

    }

    IEnumerator EndTutorial()
    {

        textA.text = "";
        TextB.text = "";
        textC1.text = "";
        textC2.text = "";
        textD1.text = "";
        textD2.text = "";
        textD3.text = "";
        textD4.text = "";
        textD5.text = "";
        textD6.text = "";
        textE1.text = "";
        textE2.text = "";
        textE3.text = "";
        textE4.text = "";
        textF1.text = "";
        textF2.text = "";
        if(skipButton) skipButton.SetActive(false);

        yield return new WaitForSeconds(5);
        textEND.enabled = true;
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        personClicking = true;
    }

    public void SkipTutorial()
    {
        Destroy(tutorialTent);
        Destroy(tutorialHuman.gameObject);
        Destroy(this.gameObject);
    }
}
