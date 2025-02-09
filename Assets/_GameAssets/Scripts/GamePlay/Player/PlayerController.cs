
using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action OnPlayerjumped;
    public event Action<PlayerState> OnPlayerStateChanged;
    [Header("Refences")]
    [SerializeField] private Transform _Orientation;

    [Header("Movement Settings")]
    [SerializeField] private KeyCode _movementKey;
    [SerializeField] private float _movementSpeed;  

    [Header("Jump Settings")]
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown;  
    [SerializeField] private float _airMultiplier;
    [SerializeField] private float _airDrag;
    [SerializeField] private bool _canJump;

    [Header("Sliding Settings")]
    [SerializeField] private KeyCode _slidingKey;
    [SerializeField] private float _slidingMultiplier;
    [SerializeField] private float _slidingDrag;

    [Header("Ground Check Settings")]

    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundDrag;


    private StateController _stateController;
    private Rigidbody _playerRigidBody;

    private float _startingMovementSpeed, _startingJumpForce;
    private float _Horizantalİnput, _Verticalİnput;

    private Vector3 _MovementDirection;

    private bool _isSliding;
    private void Awake() 
    {
        _stateController = GetComponent<StateController>();
        _playerRigidBody = GetComponent<Rigidbody>();
        _playerRigidBody.freezeRotation = true;

        _startingMovementSpeed = _movementSpeed;
        _startingJumpForce = _jumpForce;
    }
    private void Update() 
    {
        Setİnputs();
        SetStates();
        SetPlayerDrag();
        LimitPlayerSpeed();
    }
    private void FixedUpdate() 
    {
        SetPlayerMovement();     
    }
    private void Setİnputs()
    {
        _Horizantalİnput = Input.GetAxisRaw("Horizontal");
        _Verticalİnput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(_slidingKey))
        {
            _isSliding = true;
        }
        else if(Input.GetKeyDown(_movementKey))
        {
            _isSliding = false;
        }
        else if(Input.GetKeyDown(_jumpKey) && _canJump && IsGrounded())
        {
            _canJump = false;
            SetPlayerJumping();
            Invoke(nameof(ResetJumping), _jumpCooldown);
        }
    }

    private void SetStates()
    {
        var movementDirection = GetMovementDirection();
        var isGrounded = IsGrounded();
        var isSliding = IsSliding();
        var currentState = _stateController.GetCurrentState();
       
        var newState = currentState switch
        {
            _ when  movementDirection == Vector3.zero && isGrounded && !isSliding => PlayerState.Idle,
            _ when  movementDirection != Vector3.zero && isGrounded && !isSliding => PlayerState.Move,
            _ when  movementDirection != Vector3.zero && isGrounded && isSliding => PlayerState.Slide,
            _ when  movementDirection == Vector3.zero && isGrounded && isSliding => PlayerState.SlideIdle,
            _ when  !_canJump && !isGrounded => PlayerState.Jump,
            _ => currentState
        };

        if(newState != currentState)
        {
            _stateController.ChangeState(newState);
            OnPlayerStateChanged?.Invoke(newState);
        }
    }

    private void SetPlayerMovement()
    {
        _MovementDirection = _Orientation.transform.forward * _Verticalİnput + _Orientation.transform.right * _Horizantalİnput;

        float forceMultiplier = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => 1f,
            PlayerState.Slide => _slidingMultiplier,
            PlayerState.Jump => _airMultiplier,
            _ => 1f
        };
        _playerRigidBody.AddForce(_MovementDirection.normalized * _movementSpeed * forceMultiplier, ForceMode.Force);
    }
    private void SetPlayerDrag()
    {

        _playerRigidBody.linearDamping = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => _groundDrag,
            PlayerState.Slide => _slidingDrag,
            PlayerState.Jump => _airDrag,
            _ => _playerRigidBody.linearDamping
        };
    }

    private void LimitPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(_playerRigidBody.linearVelocity.x, 0f, _playerRigidBody.linearVelocity.z);

        if(flatVelocity.magnitude > _movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
            _playerRigidBody.linearVelocity = new Vector3(limitedVelocity.x, _playerRigidBody.linearVelocity.y, limitedVelocity.z);
        }
    }
    private void SetPlayerJumping()
    {
        OnPlayerjumped?.Invoke();
        _playerRigidBody.linearVelocity = new Vector3(_playerRigidBody.linearVelocity.x, 0f, _playerRigidBody.linearVelocity.z);
        _playerRigidBody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void ResetJumping()
    {
        _canJump = true;
    }
    public Rigidbody GetPlayerRigidbody()
    {
        return _playerRigidBody;
    }
    #region Helpers Functions
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);    
    }

    private Vector3 GetMovementDirection()
    {
        return _MovementDirection.normalized;
    }

    private bool IsSliding()
    {
        return _isSliding;
    }
    public void SetMovementSpeed(float speed, float duration)
    {
        _movementSpeed += speed;
       Invoke(nameof(ResetMovementSpeed), duration);
    }

    private void ResetMovementSpeed()
    {
        _movementSpeed = _startingMovementSpeed;
    }

    public void SetJumpingForce(float force, float duration)
    {
        _jumpForce += force;
        Invoke(nameof(ResetJumpingForce), duration);
    }

    private void ResetJumpingForce()
    {
        _jumpForce = _startingJumpForce;
    }
    #endregion
}
