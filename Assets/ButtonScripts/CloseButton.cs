using UnityEngine;

public class CloseButton : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;
    private void OnMouseDown()
    {
        AudioSource.PlayClipAtPoint(clickSound, transform.position, PlayerPrefs.GetFloat("SoundVolume"));
        Application.Quit();
    }
}
