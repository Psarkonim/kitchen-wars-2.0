using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("start");
        SceneManager.LoadScene("Level1");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
