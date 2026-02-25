using Mono.Cecil;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Button : MonoBehaviour, IPointerMoveHandler, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] private int spritePosition;
    [SerializeField] private Material material;
    [SerializeField] private int max = 1;
    private Action<int> adderAction;
    private Action<int> clickAction;


    public void Start()
    {
        spritePosition = 0;
        material.SetFloat("_CurrentImage", 0f);
        adderAction += EventManager.instance.player.AddResource;
        clickAction += EventManager.instance.player.AddClicks;
    }

    public void OnPointerClick(PointerEventData eventData)
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

}
