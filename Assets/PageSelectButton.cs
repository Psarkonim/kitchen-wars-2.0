using System.Linq;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PageSelectButton : MonoBehaviour
{
    [SerializeField] bool isNextPageButton;
    private int pageNumber;
    private bool toShow;

    private void Awake()
    {
        toShow = true;
        var sceneName = SceneManager.GetActiveScene().name;
        pageNumber = int.Parse(sceneName.Last().ToString());
        PlayerPrefs.SetInt("Current level", 100);

        if (pageNumber > PlayerPrefs.GetInt("Current level") && isNextPageButton) toShow = false; 
    }

    private void Start()
    {
        if (!toShow) Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Debug.Log(pageNumber);
        if (isNextPageButton)
            SceneManager.LoadScene("RecipeBookPage" + (pageNumber + 1).ToString());
        else
            SceneManager.LoadScene("RecipeBookPage" + (pageNumber - 1).ToString());
    }
}
