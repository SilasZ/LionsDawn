using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    public Scene s;
    // Start is called before the first frame update
    void Update()
    {
        if(Time.time>0.5)
        SceneManager.LoadScene("SampleScene",LoadSceneMode.Single);
    }
}
