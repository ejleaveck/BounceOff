using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class LevelController : MonoBehaviour
{

    [SerializeField] private SceneData sceneData;
    private int nextSceneIndex;
    [SerializeField] private int mainMenuSceneIndex;

    [SerializeField] private GameObject attachmentMenu;
    
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject attachmentMenuDefaultButton;
    [SerializeField] private GameObject mainMenuDefaultButton;

    [SerializeField] private Transform playerSpawner;
    
    private void OnEnable()
    {
        GameManager.LevelEndTriggered += HandleLevelEnd;

        //Game Pause Menu
        GameManager.Instance.inputHandler.OnMenuButtonPressed += OnPauseMenuButtonPressed;
    }

    private void OnDisable()
    {
        GameManager.LevelEndTriggered -= HandleLevelEnd;
        GameManager.Instance.inputHandler.OnMenuButtonPressed -= OnPauseMenuButtonPressed;
    }

    private void Start()
    {
        attachmentMenu.SetActive(false);


        
        if (sceneData.sceneIndex == mainMenuSceneIndex)
        {
            PlayerController.Instance.gameObject.SetActive(false);
        }
        else
        {
            //TODO: update this to interact with "player spawning"
            PlayerController.Instance.gameObject.SetActive(true);
        }

        MovePlayerToSpawner();
       
    }

    private void MovePlayerToSpawner()
    {
        if(playerSpawner != null && PlayerController.Instance != null)
        {
            PlayerController.Instance.gameObject.transform.position = playerSpawner.position;
        }
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


    bool isattachmentMenuActive;

    public void OnPauseMenuButtonPressed(InputAction.CallbackContext context)
    {
        isattachmentMenuActive = attachmentMenu.activeSelf;

        attachmentMenu.SetActive(!isattachmentMenuActive);

        if (mainMenu != null)
        {
            mainMenu.SetActive(isattachmentMenuActive);
        }

        if (!isattachmentMenuActive)
        {
            //Attachment menu is opened:
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(attachmentMenuDefaultButton);
        }
        else if(mainMenu !=null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(mainMenuDefaultButton);
        }
    }
      
}
