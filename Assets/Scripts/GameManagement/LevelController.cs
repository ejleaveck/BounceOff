using UnityEngine;
using UnityEngine.InputSystem;

public class LevelController : MonoBehaviour
{

    //public static LevelController Instance { get; private set; }

    [SerializeField] private SceneData sceneData;
    private int nextSceneIndex;

    [SerializeField] private PauseMenuController pauseMenuController;

    private void OnEnable()
    {
        GameManager.LevelEndTriggered += HandleLevelEnd;
    }

    private void OnDisable()
    {
        GameManager.LevelEndTriggered -= HandleLevelEnd;
    }


    //TODO: the end level method is in game manager where it is invoking the end level event. then coming here
    //needs to be updated so that when the level ends due to game play, it comes to level controller, and the level controller invokes the
    //event to allow subscribers to act accordingly, and then as here, get sent up to game manager to handle any game wide logic.
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

    public void OnPauseMenuButtonPressed(InputAction.CallbackContext context)
    {
        // Do some level controll measures to "Pause" the game and if needed relinquish player controls.

        pauseMenuController.OnPauseMenuButtonPressed(context);
    }

}
