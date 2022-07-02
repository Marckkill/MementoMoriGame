using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

public static bool GameisPaused = false; 

public GameObject PauseGameUI;

    void Update()
    
    {
    if(Input.GetKeyDown(KeyCode.Escape))
    {
        if(GameisPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    }

    public void Resume()
    {
        PauseGameUI.SetActive(false);
        Time.timeScale = 1;
        GameisPaused = false;
    }

    void Pause()
    {
        PauseGameUI.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitou do video jogo");
    }

    public void Options()
    {
        Debug.Log("Vai abaxa o volume xd"); 
    }
}

