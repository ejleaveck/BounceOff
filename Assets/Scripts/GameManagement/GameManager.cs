using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static event Action<float> GetCurrentGameTime;
        private float gameTime = .02f;

      public enum LevelEndTriggerSource
    {
        StartGame,
        GoalObject,
        Goal,
        WarpZone,
        PlayerDeath
    }

    public static event Action<LevelEndTriggerSource> LevelEndTriggered;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    private void FixedUpdate()
    {
        gameTime += Time.fixedDeltaTime;

        NotifyCurrentGameTime();
    }


    /// <summary>
    /// Call this to invoke the end of level
    /// </summary>
    /// <param name="source">The enum of what is causing the end of the level to select proper level end logic</param>
    public static void TriggerLevelEnd(LevelEndTriggerSource source)
    {
        LevelEndTriggered?.Invoke(source);
    }

    public void OnLevelComplete(int nextSceneIndex)
    {

        //do level complete stuff before loading next scene that is game wide 
        //now that game wide game management is handled send over to the scene controller for scene change specific items.
        SceneController.Instance.LoadNextScene(nextSceneIndex);
    }

    public void HandleStartGame()
    {
        TriggerLevelEnd(LevelEndTriggerSource.StartGame);
    }

    private void NotifyCurrentGameTime()
    {
        GetCurrentGameTime?.Invoke(gameTime);
    }
}
