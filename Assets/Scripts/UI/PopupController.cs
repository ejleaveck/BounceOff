using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopupController : MonoBehaviour
{
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private CanvasGroup mainMenuCanvasGroup;
    [SerializeField] private GameObject defaultPopupButton;
    [SerializeField] private GameObject defaultMainMenuButton;

    public void ShowPopup(bool show)
    {
       
        if (show)
        {
            
                mainMenuCanvasGroup.interactable = !show;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(defaultPopupButton);
            
        }
        else
        {
            mainMenuCanvasGroup.interactable = !show;

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(defaultMainMenuButton);
        }
        popupPanel.SetActive(show);
    }

    public void ClosePopup()
    {
        ShowPopup(false);
    }
}
