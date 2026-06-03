using UnityEngine;

public class LevelToMenuButton : MonoBehaviour
{
    private SceneTransition sceneTransition;

    private void Start()
    {
        sceneTransition = FindFirstObjectByType<SceneTransition>();
    }

    private void OnMouseDown()
    {
        sceneTransition.GoToScene("MainMenu");        
    }
}
