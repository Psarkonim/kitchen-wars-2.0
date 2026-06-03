using System.Linq;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PageSelectButton : MonoBehaviour
{
    [SerializeField] bool isNextPageButton;
    private int pageNumber;
    private bool toShow;
    [SerializeField] private AudioClip clickSound;
    private void Awake()
    {
        toShow = true;
        var sceneName = SceneManager.GetActiveScene().name;
        pageNumber = int.Parse(sceneName.Last().ToString());

        if (pageNumber >= PlayerPrefs.GetInt("Current level") && isNextPageButton) toShow = false; 
    }

    private void Start()
    {
        if (!toShow) gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        AudioSource.PlayClipAtPoint(clickSound, transform.position, PlayerPrefs.GetFloat("SoundVolume"));

        if (isNextPageButton)
            SceneManager.LoadScene("RecipeBookPage" + (pageNumber + 1).ToString());
        else
            SceneManager.LoadScene("RecipeBookPage" + (pageNumber - 1).ToString());
    }
}
