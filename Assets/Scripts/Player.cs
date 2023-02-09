
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private void Update()
    {
        var inputVector = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
        }
        inputVector = inputVector.normalized;
        var moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDirection * _movementSpeed * Time.deltaTime;
        var rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward,moveDirection,rotationSpeed* Time.deltaTime);
    }
}
