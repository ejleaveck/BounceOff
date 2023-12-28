using UnityEngine;

public class LevelController : MonoBehaviour
{

    public static LevelController Instance { get; private set; }

    [SerializeField] private SceneData sceneData;
    private int nextSceneIndex; 


    private void OnEnable()
    {
        GameManager.LevelEndTriggered += HandleLevelEnd;
    }

    private void OnDisable()
    {
        GameManager.LevelEndTriggered -= HandleLevelEnd;
    }

    private void HandleLevelEnd(GameManager.LevelEndTriggerSource source)
    {
        switch (source)
        {
            case GameManager.LevelEndTriggerSource.StartGame:
                GameManager.Instance.OnLevelComplete(sceneData.nextSceneIndex);
                break;
            case GameManager.LevelEndTriggerSource.GoalObject:
                nextSceneIndex = sceneData.nextSceneIndex;
                GameManager.Instance.OnLevelComplete(nextSceneIndex);
                break;
            case GameManager.LevelEndTriggerSource.Goal:
                //logic for ending based on goal
                //TODO: current goalobject logic needs to be set up in goal and then use this spot
                break;
            case GameManager.LevelEndTriggerSource.WarpZone:
                //add warpzone logic
                break;
            case GameManager.LevelEndTriggerSource.PlayerDeath:
                //add playerdeath logic
                break;
            default:
                Debug.LogError("No game ending source found, going to next level directly from SceneData as default.");
                GameManager.Instance.OnLevelComplete(sceneData.nextSceneIndex);
                break;
        }
    }

    //public void TriggerSceneEnd()
    //{
    //    //Do any scene specific end sequences that are outside the need for centralized game manager / scenecontroller.
    //    //Now those are complete and I got the next scene index from this specific scenes data send over to game manager to do game level logic.
    //    GameManager.Instance.OnLevelComplete(sceneData.nextSceneIndex);
    //}

    private void Awake()
    {
                if (Instance == null)
        {
            Instance = this;
           
        }
       
    }
    
}
