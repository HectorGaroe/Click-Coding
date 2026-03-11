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


    public void Initilice()
    {
        AutoClickerCreator();
        numActiveClickers = 0;

    }

    private void AutoClickerCreator()
    {
        clickers = new AutoClicker[numMaxClicker];
        for (int i = 0; i < numMaxClicker; i++)
        {
            GameObject clickerInstance = GameObject.Instantiate(clickerPrefab, clickerServer);
            clickers[i] = clickerInstance.GetComponent<AutoClicker>();
            clickers[i].WakeyWakey();
        }
    }

    public void ActivateClicker(Button mainButton, int clickerLvl)
    {
            
        if(numActiveClickers < numMaxClicker)
        {
            clickers[numActiveClickers].Activate(mainButton, clickerLvl);
            numActiveClickers++;
        }
    }
}
