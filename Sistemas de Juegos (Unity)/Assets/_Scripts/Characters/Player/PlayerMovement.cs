using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Component References")]
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private Camera _mainCamera;

    [Header("Other Settings")]
    [SerializeField] private LayerMask _mouseAimLayers;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float turnSpeed = 0.1f;

    //Stored Values
    [SerializeField] private Vector3 movementDirection;
    [SerializeField] private Vector3 movementDebug;
    [SerializeField] public bool isAttacking;
    private MoveCommand _moveCommand;

    private void Awake()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Start()
    {
        _moveCommand = new MoveCommand(_playerRigidbody, transform, movementDirection, movementSpeed);
    }

    public void UpdateMovementData(Vector3 newMovementDirection)
    {
        movementDirection = newMovementDirection;
    }

    void FixedUpdate()
    {
        movementDebug = CameraDirection(movementDirection);

        if (isAttacking == false)
        {
            MoveThePlayer();
            //_moveCommand.Do();
        }

        else
        {
            movementDebug = Vector3.zero;
        }

        AimTowardsMouse();
        //TurnThePlayer();
    }

    void MoveThePlayer()
    {
        var movement = CameraDirection(movementDirection) * movementSpeed * Time.deltaTime;
        movementDebug = movement;

        // Animator

        _playerRigidbody.MovePosition(transform.position + movement);
    }

    void AimTowardsMouse()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, _mouseAimLayers))
        {
            Vector3 direction = hitInfo.point - transform.position;
            direction.y = 0f;
            direction.Normalize();
            transform.forward = direction;
        }
    }

    void TurnThePlayer()
    {
        if (movementDirection.sqrMagnitude > 0.01f)
        {
            Quaternion rotation = Quaternion.Slerp(_playerRigidbody.rotation,
                                                 Quaternion.LookRotation(CameraDirection(movementDirection)),
                                                 turnSpeed);

            _playerRigidbody.MoveRotation(rotation);
        }
    }


    Vector3 CameraDirection(Vector3 movementDirection)
    {
        var cameraForward = _mainCamera.transform.forward;
        var cameraRight = _mainCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        return cameraForward * movementDirection.z + cameraRight * movementDirection.x;
    }

}