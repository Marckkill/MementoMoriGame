using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
public void PlayNewGame()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    Debug.Log("Avanca Scene 1");
}

public void QuitGame()
{
    Application.Quit();
    Debug.Log("Pediu pra sai");
}

public void OptionsMenu()
{
    Application.Quit();
    Debug.Log("Opcoes");

}
}

