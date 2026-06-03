using System.Linq;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private SceneTransition sceneTransition;
    [SerializeField] private AudioClip clickSound;

    private void Start()
    {
        sceneTransition = FindFirstObjectByType<SceneTransition>();
    }

    private void OnMouseDown()
    {
        AudioSource.PlayClipAtPoint(clickSound, transform.position, PlayerPrefs.GetFloat("SoundVolume"));

        if (PlayerPrefs.GetInt("Current level") == 1) SceneManager.LoadScene("EducationVideo");
        else
            SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("Current level").ToString());
    }
}
