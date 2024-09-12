using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSCharacterController : MonoBehaviour
{
    private Rigidbody _player;

    public float movementSpeed = 10f;
    public float jumpingPower = 5f;
    public float jumpingCooldown = 2f;
    
    private Vector3 _movement = Vector3.zero;
    private Quaternion _rotation = Quaternion.identity;
    
    private bool _jumpCooldown = false;
    
    private void Start()
    {
        _player = GetComponent<Rigidbody>();
        _rotation = _player.rotation;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void OnMove(InputValue movementValue)
    {
        var input = movementValue.Get<Vector2>();
        _movement = new Vector3(input.x, 0, input.y)  * movementSpeed;
        
        if (_movement != Vector3.zero)
        {
            _rotation = Quaternion.LookRotation(_movement);
        }
    }
    
    private void OnJump()
    {
        if (_jumpCooldown) return;
        
        _jumpCooldown = true;
        
        Invoke(nameof(ResetJumpCooldown), jumpingCooldown);
        
        _player.AddForce(Vector3.up * jumpingPower, ForceMode.Impulse);
    }
    
    private void ResetJumpCooldown()
    {
        _jumpCooldown = false;
    }
    
    private void HandleMovement()
    {
        _player.MovePosition(_player.position + _movement * Time.fixedDeltaTime);
        _player.MoveRotation(Quaternion.Slerp(_player.rotation, _rotation, Time.fixedDeltaTime * movementSpeed));
    }
}
