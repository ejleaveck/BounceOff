using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fuelText;
    [SerializeField] private Image fuelGaugeImage;
    [SerializeField] private TextMeshProUGUI attachmentText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI levelText;


    

    private void OnEnable()
    {
        // Subscribe to events
        FuelController.OnFuelChanged += UpdateFuelDisplay;
        AttachmentsController.AttachmentChanged += UpdateAttachmentDisplay;
        //LevelManager.OnLevelChanged += UpdateLevelDisplay;
        GameManager.GetCurrentGameTime += UpdateTimeDisplay;
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        FuelController.OnFuelChanged -= UpdateFuelDisplay;
        AttachmentsController.AttachmentChanged -= UpdateAttachmentDisplay;
        //LevelManager.OnLevelChanged -= UpdateLevelDisplay;
        GameManager.GetCurrentGameTime -= UpdateTimeDisplay;
    }

    private void Awake()
    {
        
    }

    private void UpdateFuelDisplay(float currentFuel, float maxFuel)
    {
        fuelText.text = $"{currentFuel.ToString("0.0")}/{maxFuel.ToString("0.0")}";

        float fillAmount = currentFuel / maxFuel;
        fuelGaugeImage.fillAmount = fillAmount;
    }

    private void UpdateAttachmentDisplay(AttachmentsController.Attachment attachmentName)
    {
        string description = AttachmentsController.GetEnumDescription(attachmentName);
        attachmentText.text = $"{description}";
    }

    private void UpdateLevelDisplay(int currentLevel)
    {
        levelText.text = $"Level: {currentLevel}";
    }

    private void UpdateTimeDisplay(float elapsedTime)
    {
        float timer = elapsedTime;
        int minutes = (int)timer / 60;
        int seconds = (int)timer % 60;
        float tenths = Mathf.Floor((timer % 1) * 10);

        string timeText = string.Format("{0:00}:{1:00}.{2:0}", minutes, seconds, tenths);

        timerText.text = timeText;
    }
}
