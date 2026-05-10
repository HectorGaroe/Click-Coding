using System;
using System.Collections;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;
    public int totalClicks;
    private int resource;
    private int[] upgradeLvls;
    private int resourcesPlus;
    private float extraTime;
    private Action<int>[] upgradeActions;
    public Action<int> OnMoreResources;
    public Action<int> OnClickPower;
    public Action<int> OnAutoClickers;
    public Action<int> OnClickerClickPower;
    public Action<int> OnLessCrash;


    private void Awake()
    {
        extraTime = 0f;
        SaveDataController.Initialize(Application.persistentDataPath);
    }

    //Sale de la aplicación forzando el guardado del progreso actual
    public void Exit()
    {
        OnSaveGame();

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    public void ResetProgress()
    {
        StartCoroutine(ResetGame());
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalClicks = SaveDataController.GetPlayerClicks();
        resource = SaveDataController.GetResources();
        upgradeLvls = SaveDataController.GetUpgradeLevels();
        upgradeActions = new Action<int>[]{
            OnMoreResources,
            OnClickPower,
            OnAutoClickers,
            OnClickerClickPower,
            OnLessCrash
        };
        ClickPower(upgradeLvls[(int)UpgradeType.ClickPower]);
        MoreResources(upgradeLvls[(int)UpgradeType.MoreResources]);
        CreateClicker(upgradeLvls[(int)UpgradeType.AutoClickers]);
        ClickerClickPower(upgradeLvls[(int)UpgradeType.ClickerClickPower]);
        TextWritter();
        InputManager.Instance.EnableInputs();
        InputManager.Instance.saveAction += OnSaveGame;
    }

    private void OnDestroy()
    {
        InputManager.Instance.saveAction -= OnSaveGame;
        InputManager.Instance.DisableInputs();
    }

    private void OnSaveGame()
    {
        SaveDataController.SetData(totalClicks, resource, Time.timeSinceLevelLoad - extraTime, upgradeLvls);
        extraTime = Time.timeSinceLevelLoad;
        SaveDataController.SaveData();
    }

    void TextWritter()
    {
        text.text = "Nº de clicks: " + totalClicks + "\nDinero: " + resource.ToString();
    }

    public int GetResources()
    {
        return resource;
    }

    public void RefreshText()
    {
        TextWritter();
    }

    public void AddResource(int amount)
    {
        resource += amount + resourcesPlus;
        TextWritter();
    }

    public void AddClicks(int numClicks)
    {
        totalClicks += numClicks;
        TextWritter();
    }

    public int GetUpgrade(UpgradeType upg)
    {
        return upgradeLvls[(int)upg];
    }
    
    public void SetUpgrade(int newLvl, UpgradeType upg)
    {
        upgradeLvls[(int)upg] += newLvl;
        upgradeActions[(int)upg]?.Invoke(upgradeLvls[(int)upg]);
    }

    public void CreateClicker(int numClickers)
    {
        SetUpgrade(numClickers, UpgradeType.AutoClickers);
    }

    public void MoreResources(int numLevels)
    {
        SetUpgrade(numLevels, UpgradeType.MoreResources);
    }

    public void ClickPower(int numLevels)
    {
        SetUpgrade(numLevels, UpgradeType.ClickPower);
    }

    public void ClickerClickPower(int numLevels)
    {
        SetUpgrade(numLevels, UpgradeType.ClickerClickPower);
    }

    public void LessCrash(int numLevels)
    {
        //SetUpgrade(numLevels, UpgradeType.LessCrash);
    }

    private IEnumerator ResetGame()
    {
        UnityEngine.SceneManagement.Scene lastScene = SceneManager.GetActiveScene();
        yield return SceneManager.LoadSceneAsync("MainMenuScene", LoadSceneMode.Additive);
        AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(lastScene);
        unloadOp.completed += op =>
        {
            SaveDataController.ResetData();
            SaveDataController.SaveData();
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenuScene"));
        };
    }
}
