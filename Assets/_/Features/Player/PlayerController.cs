using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField, BoxGroup("Components")] private HP _hp;
    [SerializeField, BoxGroup("Components")] private Rigidbody _rb;
    [SerializeField, BoxGroup("Components")] private Animator _animator;

    [SerializeField, BoxGroup("Settings")] private float _moveSpeed;
    [SerializeField, BoxGroup("Settings")] private float _waitingTimeBeforeReloadLevelOnDie;
    [SerializeField, BoxGroup("Settings")] private string _velocityParameterName;

    private void Awake()
    {
        _input = new Player_Actions();
        _input.Locomotion.Locomotion.performed += OnLocomotion_Performed;
        _input.Locomotion.Aiming.performed += OnAiming_Performed;

        _velocityParam = Animator.StringToHash(_velocityParameterName);
    }

    private void Start()
    {
        _hp.m_onDie += OnDie;
        _input.Enable();
    }

    private void OnDestroy()
    {
        _input.Locomotion.Locomotion.performed -= OnLocomotion_Performed;
        _input.Locomotion.Aiming.performed -= OnAiming_Performed;

        _hp.m_onDie -= OnDie;
    }

    private void FixedUpdate()
    {
        if (!_canMove) return;
        ApplyRotation();
        ApplyMovement();
    }

    private void OnDie()
    {
        _canMove = false;
        _input.Disable();
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;

        GameManager.m_instance.LaunchReloadLevelTimer(_waitingTimeBeforeReloadLevelOnDie);
    }

    private void ApplyMovement()
    {
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

        _animator.SetFloat(_velocityParam, Mathf.Clamp01(inputValue.magnitude));
    }

    private Player_Actions _input;
    private Vector3 _locomotionValue;
    private Vector2 _aimingValue;

    private bool _canMove = true;
    private int _velocityParam;
}