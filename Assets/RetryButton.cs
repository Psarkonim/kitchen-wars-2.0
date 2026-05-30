using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Level1");
    }
}
