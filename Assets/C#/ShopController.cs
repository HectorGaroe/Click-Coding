using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Shop : MonoBehaviour
{

    [SerializeField] Player player;
    [SerializeField] ShopSO shopSO;
    [SerializeField] UnityEngine.UI.Button[] upgradeButton;
    [SerializeField] TextMeshProUGUI[] upgradeText;
    private Action<int>[] upgradeActions;
    private Costs[] costs;
    private int[] remap;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        upgradeActions = new Action<int>[]
        {
            player.MoreResources,
            player.ClickPower,
            player.CreateClicker,
            player.ClickerClickPower,
            //player.SetUpgrade
        };

        costs = shopSO.upgradeCosts;
        int auxLength = costs.Length;
        remap = new int[auxLength];

        //Re mapea los indices del array de ShopSO a los del enum UpgradeType
        for (int i = 0; i < auxLength; i++)
        {
            Costs auxDebug = costs[i];
            Debug.Log("Upgrade type: " + auxDebug.type + "\nActual index: " + i);

            remap[i] = (int)costs[i].type;
            upgradeText[i].text = FormatButtonLabel(i);
        }
    }

    private string FormatButtonLabel(int index)
    {
        int upgrade = player.GetUpgrade((UpgradeType)index);
        int cost = costs[index].costs.Length > upgrade ? costs[index].costs[upgrade] : -1; // Si el costo para el siguiente nivel no está definido, se asigna -1
        return costs[index].buttonName + (upgrade == 0 ? "" : " Lvl" + upgrade.ToString()) + (cost == -1 ? "\nMax." : "\n<size=15px>Costo:" + cost);
    }

    public void Upgrade(int index)
    {
        int upgradeIndex = remap[index];//La posición del upgrade en el array de ShopSO, que es la que se le pasa al método, se remapea a la posición del upgrade en el enum UpgradeType, que es la que se le pasa a los métodos de Player.
        int auxCost;

        if (costs[index].costs.Length == 0) {
            Debug.LogWarning("Los costos de la mejora " + (UpgradeType)upgradeIndex + " no están definidos aún");
            return;
        }
        
        try
        {
            auxCost = costs[index].costs[player.GetUpgrade((UpgradeType)upgradeIndex)];
        } catch (IndexOutOfRangeException) {
            Debug.LogWarning("El precio de la mejora " + (UpgradeType)upgradeIndex + " de nivel " + (player.GetUpgrade((UpgradeType)upgradeIndex) + 1).ToString() + " no está definido aún");
            return;
        }

        #if UNITY_EDITOR && FALSE
        for (int i = 0; i < costs.Length; i++)
            {
                Debug.Log("Upgrade type: " + costs[i].type + "\nRemap index: " + remap[(int)costs[i].type] + "\nEnum index: " + i);
            }
        #endif

        Debug.Log("Upgrade " + (UpgradeType)upgradeIndex + " clicked.\n-Player resources: " + player.GetResources() + "\n-Upgrade cost: " + auxCost);
        if(player.GetResources() >= auxCost)
        {
            player.AddResource(-auxCost);
            player.RefreshText();
            upgradeActions[upgradeIndex]?.Invoke(1);
            string auxString = upgradeText[index].text;
            upgradeText[index].text = FormatButtonLabel(index);
            return;
        }
        Debug.Log("The player doesn't have enough resources for upgrade " + (UpgradeType)upgradeIndex + ".(You are broke twin)");
        
        upgradeButton[index].transform.DOKill(true);
        upgradeButton[index].transform.DOShakePosition(0.5f, new Vector2(7, 0), 20, 0, false, true);
    }
}
