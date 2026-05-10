using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public static class SaveDataController
{
    
    private static SaveData saveData;
    private static string persistentDataPath;

    #region Getters
        public static int GetPlayerClicks() => saveData.GetPlayerClicks();
        public static int GetResources() => saveData.GetResources();
        public static float GetPlayTime() => saveData.GetPlayTime();
        public static int[] GetUpgradeLevels() => saveData.GetUpgradeLevels();
    #endregion

    #region Setters
        public static void SetData(int playerClicks, int resources, float playTime, int[] upgradeLevels) =>
        saveData.SetData(playerClicks, resources, playTime, upgradeLevels);

        public static void SetPlayerClicksAndResources(int playerClicks, int resources) =>
            saveData.SetPlayerClicksAndResources(playerClicks, resources);
            
        public static void SetPlayTime(float playTime) => saveData.SetPlayTime(playTime);
            
        public static void SetUpgradeLevels(int[] upgradeLevels) => saveData.SetUpgradeLevels(upgradeLevels);
    #endregion

    public static void ResetData() => saveData = new SaveData();

    public static void Initialize(string newPersistentDataPath)
    {
        persistentDataPath = newPersistentDataPath;
        LoadData();
    }

    public static void LoadData()
    {

        try
        {
            string filePath = Path.Combine(persistentDataPath, "ClickCoding", "saves", "saveData.json");
            if(!File.Exists(filePath))
            {
                // Si no existe el archivo, se crea un nuevo objeto SaveData con valores predeterminados
                saveData = new SaveData();
                return;
            }
            string json = File.ReadAllText(filePath);
            saveData = JsonConvert.DeserializeObject<SaveData>(json);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Loading error {e.Message}");
            Application.Quit();
        }
    }

    public static void SaveData()
    {
        try
        {
            string directoryPath = Path.Combine(persistentDataPath, "ClickCoding", "saves");
            if (!Directory.Exists(directoryPath))
            {
                // Si no existe el directorio, se crea un nuevo directorio
                Directory.CreateDirectory(directoryPath);
            }
            string json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
            string filePath = Path.Combine(directoryPath, "saveData.json");
            File.WriteAllText(filePath, json);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Saving error {e.Message}");
            Application.Quit();
        }
    }

}
