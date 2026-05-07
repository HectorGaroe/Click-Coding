using UnityEngine;
using System;
using CTX = UnityEngine.InputSystem.InputAction.CallbackContext;

public class InputManager : MonoBehaviour
{
    private InputSystem_Actions inputSystem;
    public static InputManager Instance;
    public Action saveAction;

    private void Awake()
    {
        if (Instance == null)
        {
            inputSystem = new InputSystem_Actions();
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    public void EnableInputs()
    {
        inputSystem.Enable();
        inputSystem.Player.SaveGame.started += OnSaveGame;
    }
    
    public void DisableInputs()
    {
        inputSystem.Disable();
        inputSystem.Player.SaveGame.started -= OnSaveGame;
    }

    private void OnSaveGame(CTX ctx) => saveAction?.Invoke();

    
}
