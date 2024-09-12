using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSCharacterController : MonoBehaviour
{
    private Rigidbody _player;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    
    private Vector2 _movementInput;
    
    private bool _isJumping;
    
    private void Start()
    {
        _player = GetComponent<Rigidbody>();
    }
    
    private void OnMove(InputValue value)
    {
        _movementInput = value.Get<Vector2>();
    }
    
    private void OnJump(InputValue value)
    {
        _isJumping = value.isPressed;
    }
    
    private void FixedUpdate()
    {
        var movement = new Vector3(_movementInput.x, 0, _movementInput.y) * _speed;
        movement = transform.TransformDirection(movement);
        _player.velocity = new Vector3(movement.x, _player.velocity.y, movement.z);
        
        if (_isJumping)
        {
            _player.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isJumping = false;
        }
    }
    
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isJumping = false;
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isJumping = true;
        }
    }
    
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isJumping = false;
        }
    }
}