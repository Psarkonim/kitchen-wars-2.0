using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevelButton : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;

    private void OnMouseDown()
    {
        AudioSource.PlayClipAtPoint(clickSound, transform.position, PlayerPrefs.GetFloat("SoundVolume"));
        var currentLevel = !PlayerPrefs.HasKey("Current level") ? 1 : PlayerPrefs.GetInt("Current level");
        SceneManager.LoadScene("Level" + currentLevel.ToString());
    }
}
