using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Current level"))
            PlayerPrefs.SetInt("Current level", 1);

        if (!PlayerPrefs.HasKey("SoundVolume"))
            PlayerPrefs.SetFloat("SoundVolume", 0.75f);

        if (!PlayerPrefs.HasKey("MusicVolume"))
            PlayerPrefs.SetFloat("MusicVolume", 0.75f);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
