using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameButtonController : MonoBehaviour
{
    public Button startButton;

    void Start()
    {
     if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
    }


    void OnDestroy()
    {
        if(startButton != null)
        {
            startButton.onClick.RemoveListener(OnStartButtonClicked);
        }
    }

    private void OnStartButtonClicked()
    {
        GameManager.Instance.HandleStartGame();
   
    }
}
