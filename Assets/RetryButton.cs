using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        var currentLevel = PlayerPrefs.HasKey("Current level") ? 1 : PlayerPrefs.GetInt("Current level");
        SceneManager.LoadScene("Level" + currentLevel.ToString());
    }
}
