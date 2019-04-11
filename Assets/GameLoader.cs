using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    bool firstUpdate = true;
    void Update()
    {
        if (firstUpdate)
        {
            firstUpdate = false;
            StartCoroutine(StartGame());
        } 
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.1F);
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    
}
