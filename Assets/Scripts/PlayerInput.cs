using System;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    public Vector2 InputVector { get; private set; }
    private PlayerInputSystem _playerInputSystem;
    public EventHandler OnInteractListenEvent;
    private void Awake()
    {
        _playerInputSystem = new PlayerInputSystem();
        _playerInputSystem.PlayerInput.Enable();
        _playerInputSystem.PlayerInput.Interact.performed += Interact_performed; ;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractListenEvent?.Invoke(this,EventArgs.Empty);
    }

    void Update()
    {
        InputVector = GetNormalizedInputVector();
    }

    Vector2 GetNormalizedInputVector()
    {
        var inputAction = _playerInputSystem.PlayerInput.Move;
        var inputVector = inputAction.ReadValue<Vector2>();
        return inputVector;
    }
}
