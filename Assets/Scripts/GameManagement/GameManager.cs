using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerInputHandler inputHandler;
    public SceneController sceneController;
    public DataManager dataManager;

    public GameData CurrentGameData {  get; private set; }

    public static event Action<float> OnGameTimeChange;
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
            inputHandler = GetComponent<PlayerInputHandler>();
            sceneController = GetComponent<SceneController>();
            dataManager = GetComponent<DataManager>();

            CurrentGameData = new GameData();

            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }


        //TODO: Initiate game loading

        dataManager.LoadInitialGameData();

        

        if(dataManager.initialGameData.savedGameExists)
        {
            //TODO: Load GameData, and transfer to CurrentGameData
        }
      
       
    }


    private void FixedUpdate()
    {
        gameTime += Time.fixedDeltaTime;

        NotifyCurrentGameTime();
    }


    //TODO: Get this to be in the level controller so it goes straight from source object (goal, warp, etc) to the level controller,
    //then it can call OnLevelComplete as it does now.
    /// <summary>
    /// Call this to invoke the end of level
    /// </summary>
    /// <param name="source">The enum of what is causing the end of the level to select proper level end logic</param>
    public static void TriggerLevelEnd(LevelEndTriggerSource source)
    {
        LevelEndTriggered?.Invoke(source);
    }


    /// <summary>
    /// This is called in sequence of the game end logic after level Controller.
    /// </summary>
    /// <param name="nextSceneIndex"></param>
    public void OnLevelComplete(int nextSceneIndex)
    {

        //do level complete stuff before loading next scene that is game wide 
        //now that game wide game management is handled send over to the scene controller for scene change specific items.
        Instance.sceneController.LoadNextScene(nextSceneIndex);
    }

    public void HandleStartGame()
    {
        TriggerLevelEnd(LevelEndTriggerSource.StartGame);
    }

    private void NotifyCurrentGameTime()
    {
        OnGameTimeChange?.Invoke(gameTime);
    }
}
