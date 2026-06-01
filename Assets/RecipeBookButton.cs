using UnityEngine;
using UnityEngine.SceneManagement;

public class RecipeBookButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        EnterScene("RecipeBookPage1");
    }

    public static async void EnterScene(string sceneName)
    {
        var currentScene = SceneManager.GetActiveScene();
        foreach (var obj in currentScene.GetRootGameObjects())
            obj.SetActive(false); 

        await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }
}
