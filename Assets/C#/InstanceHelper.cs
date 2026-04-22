using Unity.VisualScripting;
using UnityEngine;
using System;

[Serializable]
public class InstanceHelper
{

    [SerializeField] GameObject clickerPrefab;
    [SerializeField] Transform clickerServer;
    [SerializeField] int numMaxClicker;

    private AutoClicker[] clickers;
    private int numActiveClickers;
    private int clickerClickPwr;


    public void Initilice()
    {
        AutoClickerCreator();
        numActiveClickers = 0;
        clickerClickPwr = 0;
    }

    private void AutoClickerCreator()
    {
        clickers = new AutoClicker[numMaxClicker];
        for (int i = 0; i < numMaxClicker; i++)
        {
            GameObject clickerInstance = GameObject.Instantiate(clickerPrefab, clickerServer);
            clickers[i] = clickerInstance.GetComponent<AutoClicker>();
            clickers[i].WakeyWakey(clickerClickPwr);
        }
    }

    public void ActivateClicker(Button mainButton, int clickerClickPower)
    {
            
        if(numActiveClickers < numMaxClicker)
        {
            clickers[numActiveClickers].Activate(mainButton, clickerClickPower);
            numActiveClickers++;
        }
    }

    public void AutoClickerPwrUpgrader(int newClickPwrLvl)
    {
        clickerClickPwr = newClickPwrLvl;
        for (int i = 0; i < numMaxClicker; i++)
        {
            clickers[i].OnRefreshLvl(newClickPwrLvl);
        }
    }
}
