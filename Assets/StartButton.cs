using System.Linq;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private SceneTransition sceneTransition;

    private void Start()
    {
        sceneTransition = FindFirstObjectByType<SceneTransition>();
    }

    private void OnMouseDown()
    {
        PlayerPrefs.SetInt("Current level", 1);

        if (!PlayerPrefs.HasKey("Current level"))
            PlayerPrefs.SetInt("Current level", 1);

        if (!PlayerPrefs.HasKey("SoundVolume"))
            PlayerPrefs.SetFloat("SoundVolume", 1.0f);

        SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("Current level").ToString());
    }
}
