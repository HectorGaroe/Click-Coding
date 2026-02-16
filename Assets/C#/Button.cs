using Mono.Cecil;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Button : MonoBehaviour, IPointerMoveHandler, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] private int contClick;
    [SerializeField] private int spritePosition;
    //[SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Material material;
    [SerializeField] private int max = 1;
    [SerializeField] public bool getsMoney;


    public void Start()
    {
        contClick = 0;
        spritePosition = 0;
        //text.text = "0";
        material.SetFloat("_CurrentImage", 0f);
    }

    public string getClicks()
    {
        return contClick.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        contClick += 1;
        //text.text = contClick.ToString();
        spritePosition += 1;


        spritePosition = spritePosition % max;
        if (spritePosition == 0)
            getsMoney = true;
        
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
