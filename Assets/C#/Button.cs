using Mono.Cecil;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Button : MonoBehaviour, IPointerMoveHandler, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] InstanceHelper instanceHelper;
    
    [SerializeField] private int spritePosition;
    [SerializeField] private Material material;
    [SerializeField] private int max = 1;
    
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
        spritePosition += 1;


        spritePosition = spritePosition % max;
        if (spritePosition == 0)
            adderAction?.Invoke(1);

        material.SetFloat("_CurrentImage", (float)spritePosition);//El String se refiere a la propiedad del shader
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }


    #region Upgrade Methods

    public void OnMoreResources(int newLvl)
    {
        
    }
    
    public void OnClickPower(int newLvl)
    {
        
    }
    
    public void OnAutoClickers(int newLvl)
    {
        instanceHelper.ActivateClicker(this, newLvl);
    }
    
    public void OnClickerClickPower(int newLvl)
    {
        
    }
    
    public void OnLessCrash(int newLvl)
    {
        
    }

    #endregion
}
