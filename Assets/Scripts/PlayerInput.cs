using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    public Vector2 InputVector { get; private set; }
    private PlayerInputSystem _playerInputSystem;
    private void Awake()
    {
        _playerInputSystem = new PlayerInputSystem();
        _playerInputSystem.PlayerInput.Enable();
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
