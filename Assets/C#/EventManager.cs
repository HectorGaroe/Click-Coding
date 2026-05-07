using UnityEngine;

public class EventManager : MonoBehaviour
{
    
    public static EventManager instance;
    public Player player;

    private void Awake()
    {
        instance = this;
    }
}
