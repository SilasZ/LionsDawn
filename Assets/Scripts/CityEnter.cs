using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityEnter : MonoBehaviour
{
    public AreaEffector2D wind;
    public AudioSource theme;
    public Text startText;
    bool leavingFirstTime = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            wind.enabled = false;
            theme.GetComponent<Music>().inCity = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            wind.enabled = true;
            theme.GetComponent<Music>().inCity = false;
        }

        if (leavingFirstTime)
        {
            StartCoroutine(StartGame());
            leavingFirstTime = false;
        }
    }

    IEnumerator StartGame()
    {
        startText.enabled = true;
        startText.text= "And so you cast off to find some lost souls!";
        yield return new WaitForSeconds(4);
        startText.enabled = false;
    }
}
