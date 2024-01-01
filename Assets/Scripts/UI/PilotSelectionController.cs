using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotSelectionController : MonoBehaviour
{

    public GameObject rightPFP_Panel;
    public GameObject leftPFP_Panel;

    private GameObject selectedPFP_Panel = null;


    void Start()
    {
        if (rightPFP_Panel != null || leftPFP_Panel != null)
        {
            rightPFP_Panel.SetActive(false);
            leftPFP_Panel.SetActive(false);
        }
    }

    public void SelectRightPilot()
    {
        rightPFP_Panel.SetActive(true);
        leftPFP_Panel.SetActive(false);
        selectedPFP_Panel = rightPFP_Panel;
    }

    public void SelectLeftPilot()
    {
        leftPFP_Panel.SetActive(true);
        rightPFP_Panel.SetActive(false);
        selectedPFP_Panel = leftPFP_Panel;
    }

    public void OnLaunchButtonSelected()
    {
        if(selectedPFP_Panel != null)
        {
            selectedPFP_Panel.SetActive(true);
        }
    }
}
