using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;
    private void OnMouseDown()
    {
        AudioSource.PlayClipAtPoint(clickSound, transform.position, PlayerPrefs.GetFloat("SoundVolume"));
        SceneManager.LoadScene("MainMenu");
    }
}
