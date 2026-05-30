using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Level1");
    }
}
