using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;
    public int totalClicks;
    private int resource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextWritter();
        resource = 0;
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
}
