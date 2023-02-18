
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private PlayerInput _inputSystem;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _counterLayerMask;


    private Vector3 _lastInteractDirection;
    public bool IsWalking { get; private set; }
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _inputSystem = GetComponentInChildren<PlayerInput>();
        _inputSystem.OnInteractListenEvent += PlayerInput_EventHandler;
    }

    private void PlayerInput_EventHandler(object sender, EventArgs e)
    {
        var inputVector = _inputSystem.InputVector;
        Vector3 moveDir = new(inputVector.x, 0, inputVector.y);
        float interactDistance = 2f;
        if (moveDir != Vector3.zero)
        {
            _lastInteractDirection = moveDir;
        }
        if (Physics.Raycast(transform.position, _lastInteractDirection, out RaycastHit hitInfo, interactDistance, _counterLayerMask))
        {
            if (hitInfo.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }
    }

    private void Update()
    {
        HandleMovement();
        //HandleInteract();
    }

    private void HandleMovement()
    {
        var inputVector = _inputSystem.InputVector;
        CheckWalking(inputVector);
        Vector3 moveDir = new(inputVector.x, 0, inputVector.y);

        var moveDistance = _movementSpeed * Time.deltaTime;
        var playerRadius = 0.7f;
        var playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        if (!canMove)
        {
            //Cann't move toward move dir
            //Attemp only x movement
            var moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                //Can move only on the x
                moveDir = moveDirX;
            }
            else
            {
                //Attemp on z movement
                var moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.forward * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDistance * moveDir;
        }
        var rotationSpeed = 20f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);
    }

    private void CheckWalking(Vector2 inputVector)
    {
        IsWalking = inputVector != Vector2.zero;
    }
}
