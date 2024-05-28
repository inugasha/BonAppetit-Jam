using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _moveSpeed;    

    private void Awake()
    {
        _input = new Player_Actions();
        _input.Locomotion.Locomotion.performed += OnLocomotion_Performed;
        _input.Locomotion.Aiming.performed += OnAiming_Performed;
    }

    private void Start()
    {
        _input.Enable();
    }

    private void OnDestroy()
    {
        _input.Locomotion.Locomotion.performed -= OnLocomotion_Performed;
        _input.Locomotion.Aiming.performed -= OnAiming_Performed;
    }

    private void FixedUpdate()
    {
        if (!_canMove) return;
        ApplyRotation();
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        Debug.Log(_locomotionValue);
        _rb.velocity = _locomotionValue * _moveSpeed;
    }

    private void ApplyRotation()
    {
        Vector3 targetRotation = new Vector3(_aimingValue.x, 0, _aimingValue.y);
        if (targetRotation == Vector3.zero) targetRotation = transform.forward;

        Quaternion newRotation = Quaternion.LookRotation(targetRotation);
        transform.rotation = newRotation;
    }

    private void OnAiming_Performed(InputAction.CallbackContext context)
    {
        _aimingValue = context.ReadValue<Vector2>();
    }

    private void OnLocomotion_Performed(InputAction.CallbackContext context)
    {
        Vector2 inputValue = context.ReadValue<Vector2>();
        _locomotionValue = new Vector3(inputValue.x, 0, inputValue.y);
    }

    private Player_Actions _input;
    private Vector3 _locomotionValue;
    private Vector2 _aimingValue;

    private bool _canMove = true;
}