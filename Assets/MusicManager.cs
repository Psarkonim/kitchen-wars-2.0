using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private static string instanceType;
    private AudioSource audioSource;

    [Header("Настройки затухания")]
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private List<string> sceneNamesToPlay;
    [SerializeField] string type;

    private void Awake()
    {
        if (instance != null && instanceType == type)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        instanceType = type;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        audioSource.Play();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");

        if (!sceneNamesToPlay.Any(sceneName => scene.name.StartsWith(sceneName)))
        {
            audioSource.Stop();
            Destroy(gameObject);
        }
    }
}