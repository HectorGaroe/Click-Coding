using UnityEngine;
using UnityEngine.UI;

public class AutoClicker : MonoBehaviour
{
    private const int FINAL_SPRITE = 11;

    [SerializeField] Material instanceMaterial;
    [SerializeField] Image progressImage;
    private Material material;
    private Button mainButton;
    private float cycleTime;
    private float nextClick;
    private float progressTime;
    private readonly float[] cycleTimes = new float[] { 10f, 7f, 6f, 4f, 2f, 0.01f };//Ejemplo de tiempos de ciclo para cada nivel

    public void WakeyWakey()
    {
        progressImage.material = new Material(instanceMaterial);
        material = progressImage.material;
        gameObject.SetActive(false);
    }

    public void Activate(Button mainButton, int cycleLvl)
    {
        OnRefreshLvl(cycleLvl);
        this.mainButton = mainButton;
        this.mainButton.onUpdate += OnUpdate;
        this.mainButton.onClickerUpgrade += OnRefreshLvl;
        nextClick = Time.time + cycleTime;//Tiempo hasta proximo click
        progressTime = 0;
        gameObject.SetActive(true);
    }

    public void OnUpdate(float deltaTime)
    {
        if(Time.time >= nextClick)
        {
            nextClick += cycleTime;
            progressTime = 0;
            mainButton.Click();
        }
        Progress(deltaTime);
    }

    public void OnRefreshLvl(int cycleLvl)
    {
        //Aqui tabla de nivel -> tiempo de ciclo
        cycleTime = cycleTimes[cycleLvl];
    }

    //Si midiesemos esto como lo habiamos hecho con el mainButton tendriamos que hacer que en la primera mejora(por ejemplo) el sprite saltara de dos en dos,
    //y claro nosotros queremos que el progreso se vea en el sprite completamente?(como sube la barrita)
    void Progress(float deltaTime)
    {
        int progressPrcnt;
        float auxTime;

        progressTime += deltaTime;

        auxTime = progressTime / cycleTime;
        auxTime = Mathf.Clamp01(auxTime);
        auxTime = auxTime * FINAL_SPRITE;
        progressPrcnt = Mathf.FloorToInt(auxTime);
        
        material.SetFloat("_CurrentImage", (float)progressPrcnt);//El String se refiere a la propiedad del shader
    }
}
 