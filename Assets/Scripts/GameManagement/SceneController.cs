using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    private Dictionary<int, string> sceneDictionary = new Dictionary<int, string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeSceneDictionary();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSceneDictionary()
    {
        sceneDictionary.Add(1, "A-MainMenu");
        sceneDictionary.Add(2, "Level00-00");
        sceneDictionary.Add(3, "Level01-01");
    }

    public void LoadNextScene(int nextSceneIndex)
    {
        if (sceneDictionary.TryGetValue(nextSceneIndex, out string sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            //TODO: Update this to do something more useful for the player if there is indeed a scene load error.
            Debug.LogError("Scene index not found. Loaded Main Menu");
            SceneManager.LoadScene("A-MainMenu");
        }
    }


}
