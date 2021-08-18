using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private float _groundCheckDistance = 0.4f;
    [SerializeField] private LayerMask _groundLayerMask;

    private float gravity = -9.81f;
    private Vector3 velocity;
    private bool isGrounded;

    /// ==========[ Properties ]==========
    float Speed => _speed;
    float JumpForce => _jumpForce;

    /// ==========[ Unity Events ]==========
    private void Update()
    {
        isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckDistance, _groundLayerMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        Vector3 moveBy = transform.right * inputX + transform.forward * inputZ;

        _characterController.Move(moveBy.normalized * Speed * Time.deltaTime); // Moving.

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpForce * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        _characterController.Move(velocity * Time.deltaTime); // Falling.
    }
}
