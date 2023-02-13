
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private PlayerInput _inputSystem;
    [SerializeField] private Animator _animator;
    public bool IsWalking { get; private set; }
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _inputSystem = GetComponentInChildren<PlayerInput>();
    }

    private void Update()
    {
        var inputVector = _inputSystem.InputVector;
        CheckWalking(inputVector);
        var moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDirection * _movementSpeed * Time.deltaTime;
        var rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward,moveDirection,rotationSpeed* Time.deltaTime);
    }

    private void CheckWalking(Vector2 inputVector)
    {
        IsWalking = inputVector != Vector2.zero ? true: false;
    }
}
