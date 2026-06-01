using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance { get; private set; }

    // Кэш объектов сцены
    private Dictionary<string, GameObject[]> _savedScenes = new Dictionary<string, GameObject[]>();
    private string _previousSceneName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GoToScene(string newSceneName)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        _previousSceneName = currentScene.name;

        // Сохраняем и прячем текущую сцену
        if (!_savedScenes.ContainsKey(_previousSceneName))
        {
            _savedScenes[_previousSceneName] = currentScene.GetRootGameObjects();
        }

        foreach (GameObject obj in _savedScenes[_previousSceneName])
        {
            if (obj != gameObject) obj.SetActive(false);
        }

        // Загружаем новую сцену поверх
        SceneManager.LoadScene(newSceneName, LoadSceneMode.Additive);
    }

    public void GoBack()
    {
        // Выгружаем текущую сцену
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        // Показываем предыдущую
        if (_savedScenes.ContainsKey(_previousSceneName))
        {
            foreach (GameObject obj in _savedScenes[_previousSceneName])
            {
                if (obj != null) obj.SetActive(true);
            }
        }

        // Делаем её активной
        Scene previousScene = SceneManager.GetSceneByName(_previousSceneName);
        if (previousScene.isLoaded)
        {
            SceneManager.SetActiveScene(previousScene);
        }
    }
}