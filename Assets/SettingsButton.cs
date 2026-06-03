using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;
    private void OnMouseDown()
    {
        AudioSource.PlayClipAtPoint(clickSound, transform.position, PlayerPrefs.GetFloat("SoundVolume"));
        SceneManager.LoadScene("Settings");
    }
}

