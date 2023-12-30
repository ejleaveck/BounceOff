using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{
    public GameObject attachmentMenu;

    public GameObject defaultSelectedObject;


    // Start is called before the first frame update
    void Start()
    {
        attachmentMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPauseMenuButtonPressed(InputAction.CallbackContext context )
    {
        gameObject.SetActive(!gameObject.activeSelf);

if (gameObject.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(defaultSelectedObject);
        }
    }
}
