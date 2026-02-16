using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private Button mainButton;
    [SerializeField] private TextMeshProUGUI text;
    public int totalClicks;
    private int resource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textWritter();
        resource = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainButton.getsMoney)
        {
            resource += 1;
            mainButton.getsMoney = false;
        }
        textWritter();
    }

    void textWritter()
    {
        text.text = "Nº de clicks" + mainButton.getClicks() + "\nDinero: " + resource.ToString();
    }
}
