using System;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;
    public int totalClicks;
    private int resource;
    private int[] upgradeLvls;
    private Action<int>[] upgradeActions;
    public Action<int> OnMoreResources;
    public Action<int> OnClickPower;
    public Action<int> OnAutoClickers;
    public Action<int> OnClickerClickPower;
    public Action<int> OnLessCrash;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextWritter();
        resource = 0;
        upgradeLvls = new int[(int)UpgradeType.TYPES];
        upgradeActions = new Action<int>[]{
            OnMoreResources,
            OnClickPower,
            OnAutoClickers,
            OnClickerClickPower,
            OnLessCrash
        };
    }

    void TextWritter()
    {
        text.text = "Nº de clicks: " + totalClicks + "\nDinero: " + resource.ToString();
    }

    public void AddResource(int amount)
    {
        resource += amount;
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
        upgradeActions[(int)upg]?.Invoke(newLvl);
        upgradeLvls[(int)upg] = newLvl;
    }

    public void CreateClickerDEBUG(int numClickers)
    {
        SetUpgrade(numClickers, UpgradeType.AutoClickers);
    }
}
