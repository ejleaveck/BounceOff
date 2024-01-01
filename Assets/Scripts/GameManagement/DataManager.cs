using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public InitialGameData initialGameData;

    private string saveFilePath;

    //TODO: Consider making a UI Manger script to attach to game manager object to separate this type of stuff
    [SerializeField] private PopupController popupController;

    private void Awake()
    {
        popupController.ShowPopup(false);

        saveFilePath = Path.Combine(Application.persistentDataPath, "initialGameData.json");
        
    }

    //TODO: jsut a note taht this will need to be called on an awake or something method to start the process

    /// <summary>
    /// Load InitialGameData to initialize proper branches of logic in game start.
    /// </summary>
      public void LoadInitialGameData()
    {
        if (File.Exists(saveFilePath))
        {
            string jsonData = File.ReadAllText(saveFilePath);

            Debug.Log($"{saveFilePath}");
            
            initialGameData = JsonUtility.FromJson<InitialGameData>(jsonData);
        }
        else
        {
            initialGameData = new InitialGameData();
            SaveInitialGameData();

            popupController.ShowPopup(true);
        }

    }

    public void SaveInitialGameData()
    {
        string jsonData = JsonUtility.ToJson(initialGameData);
        File.WriteAllText(saveFilePath, jsonData);
    }
}
