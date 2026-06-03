using UnityEngine;
using UnityEngine.SceneManagement;

public class RecipeBookButton : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;

    private void OnMouseDown()
    {
        AudioSource.PlayClipAtPoint(clickSound, transform.position, PlayerPrefs.GetFloat("SoundVolume"));
        SceneManager.LoadScene("RecipeBookPage1");
    }
}
