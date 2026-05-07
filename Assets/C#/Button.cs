using Mono.Cecil;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Button : MonoBehaviour, IPointerMoveHandler, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] InstanceHelper instanceHelper;
    
    [SerializeField] private int spritePosition;
    [SerializeField] private Material material;
    [SerializeField] private int maxSprite = 1;
    
    private int resourcesPerCycle;
    private int clickPower;

    //Info saver events
    private Action<int> adderAction;
    private Action<int> clickAction;

    //Clicker events
    public Action<float> onUpdate;
    public Action<int> onClickerUpgrade;



    public void Awake()
    {
        instanceHelper.Initilice();
    }

    public void Start()
    {
        spritePosition = 0;
        resourcesPerCycle = 1;
        clickPower = 1;
        material.SetFloat("_CurrentImage", 0f);
        adderAction += EventManager.instance.player.AddResource;
        clickAction += EventManager.instance.player.AddClicks;

        //Upgrades
        EventManager.instance.player.OnMoreResources += OnMoreResources;
        EventManager.instance.player.OnClickPower += OnClickPower;
        EventManager.instance.player.OnAutoClickers += OnAutoClickers;
        EventManager.instance.player.OnClickerClickPower += OnClickerClickPower;
        EventManager.instance.player.OnLessCrash += OnLessCrash;

    }

    public void Update()
    {
        onUpdate?.Invoke(Time.deltaTime);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Click();
    }

    public void Click()
    {
        clickAction?.Invoke(1);
        spritePosition += clickPower;

        if (spritePosition >= maxSprite)
            AddResources();
        spritePosition = spritePosition % maxSprite;

        material.SetFloat("_CurrentImage", (float)spritePosition);//El String se refiere a la propiedad del shader
    }

    public void ClickerClick(int clickerPwrLvl)
    {
        spritePosition += clickerPwrLvl;
        
        if (spritePosition >= maxSprite)
            AddResources();
        spritePosition = spritePosition % maxSprite;

        material.SetFloat("_CurrentImage", (float)spritePosition);//El String se refiere a la propiedad del shader
    }

    private void AddResources()
    {
        int max = (spritePosition / maxSprite);
        for (int cont = 0; cont < max; cont++)
        {
            adderAction?.Invoke(resourcesPerCycle);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Cuando el raton entra en el boton
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Cuando el raton sale del boton
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        //Cuando el raton se mueve dentro del boton
    }


    #region Upgrade Methods

    public void OnMoreResources(int numLvls)
    {
        // newLvl = the level of the upgrade; if 1 -> obtain 2 resources per cycle
        resourcesPerCycle = 1 + numLvls;
    }
    
    public void OnClickPower(int numLvls)
    {
        clickPower = 1 + numLvls;
    }
    
    public void OnAutoClickers(int newClickerClickPowerLvl)
    {
        instanceHelper.ActivateClicker(this, newClickerClickPowerLvl);
    }
    
    public void OnClickerClickPower(int newClickPwrLvl)
    {
        instanceHelper.AutoClickerPwrUpgrader(newClickPwrLvl);
    }
    
    public void OnLessCrash(int newLvl)
    {
        
    }

    #endregion
}
