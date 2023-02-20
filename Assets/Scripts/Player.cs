
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public EventHandler<OnSelectedCounterArg> OnSelectedCounterChange;
    public class OnSelectedCounterArg : EventArgs
    {
        public ClearCounter SelectedCounter;
    }

    [SerializeField] private float _movementSpeed;
    [SerializeField] private PlayerInput _inputSystem;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _counterLayerMask;
    [SerializeField] private ClearCounter _selectedCounter;

    private Vector3 _lastInteractDir;

    public bool IsWalking { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        } 
    }
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _inputSystem = GetComponentInChildren<PlayerInput>();
        _inputSystem.OnInteractListenEvent += PlayerInput_EventHandler;
    }

    private void PlayerInput_EventHandler(object sender, EventArgs e)
    {
        if (_selectedCounter)
        {
            _selectedCounter.Interact();
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteract();
    }

    private void HandleInteract()
    {
        var inputVector = _inputSystem.InputVector;
        Vector3 moveDir = new(inputVector.x, 0, inputVector.y);
        if(moveDir != Vector3.zero)
        {
            _lastInteractDir= moveDir;
        }
        float interactDistance = 1f;
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, interactDistance, _counterLayerMask))
        {
            if (hitInfo.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if(clearCounter != _selectedCounter)
                {
                    _selectedCounter = clearCounter;
                    OnSelectedCounterChange?.Invoke(this, new()
                    {
                        SelectedCounter = _selectedCounter
                    });
                }
                else {
                    _selectedCounter = null;
                }
            }
            else
            {
                _selectedCounter= null;
            }
        }
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
