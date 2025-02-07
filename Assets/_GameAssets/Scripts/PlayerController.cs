
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Refences")]
    [SerializeField] private Transform _Orientation;

    [Header("Movement Settings")]
    [SerializeField] private KeyCode _movementKey;
    [SerializeField] private float _movementSpeed;  

    [Header("Jump Settings")]
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown;  
    [SerializeField] private bool _canJump;

    [Header("Sliding Settings")]
    [SerializeField] private KeyCode _slidingKey;
    [SerializeField] private float _slidingMultiplier;
    [SerializeField] private float _slidingDrag;

    [Header("Ground Check Settings")]

    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundDrag;

    private Rigidbody _playerRigidBody;

    private float _Horizantalİnput, _Verticalİnput;

    private Vector3 _MovementDirection;

    private bool _isSliding;
    private void Awake() 
    {
        _playerRigidBody = GetComponent<Rigidbody>();
        _playerRigidBody.freezeRotation = true;
    }
    private void Update() 
    {
        Setİnputs();
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

    private void SetPlayerMovement()
    {
        _MovementDirection = _Orientation.transform.forward * _Verticalİnput + _Orientation.transform.right * _Horizantalİnput;

        if(_isSliding)
        {
            _playerRigidBody.AddForce(_MovementDirection.normalized * _movementSpeed * _slidingMultiplier, ForceMode.Force);
        }
        else
        {
            _playerRigidBody.AddForce(_MovementDirection.normalized * _movementSpeed, ForceMode.Force);
        }        
    }
    private void SetPlayerDrag()
    {
        if(_isSliding)
        {
            _playerRigidBody.linearDamping = _slidingDrag;
        }
        else
        {
            _playerRigidBody.linearDamping = _groundDrag;
        }
        
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
        _playerRigidBody.linearVelocity = new Vector3(_playerRigidBody.linearVelocity.x, 0f, _playerRigidBody.linearVelocity.z);
        _playerRigidBody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void ResetJumping()
    {
        _canJump = true;
    }
    
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);    
    }
}
