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
        Debug.Log("Sprite Position: " + spritePosition);

        //TODO: Plantear una logica en el reinicio del ciclo para que no de problemas con el click power,
        //por ejemplo si el click power es 2 y el max sprite es 5, el ciclo se reiniciaria en 4, luego 6 (que se reinicia a 0) y luego 2
        spritePosition = spritePosition % maxSprite;//Hay que asegurarse de que el sprite position no se pase del maximo, si se pasa vuelve a 0 y empieza de nuevo el ciclo
        if (spritePosition == 0 || spritePosition > maxSprite)
            adderAction?.Invoke(resourcesPerCycle);

        material.SetFloat("_CurrentImage", (float)spritePosition);//El String se refiere a la propiedad del shader
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
        resourcesPerCycle += numLvls;
    }
    
    public void OnClickPower(int numLvls)
    {
        if(clickPower == 1)
        {
            clickPower = 2;
            Debug.Log("Click Power: " + clickPower);
            return;
        }
        clickPower += numLvls;//Tiene que ser un numero par para que no de problemas el reinicio del ciclo
        Debug.Log("Click Power: " + clickPower);
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
