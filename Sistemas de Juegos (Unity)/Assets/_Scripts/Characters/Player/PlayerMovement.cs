using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Component References")]
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Camera mainCamera;

    [Header("Other Settings")]
    [SerializeField] private LayerMask _mouseAimLayers;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float turnSpeed = 0.1f;

    //Stored Values
    [SerializeField] private Vector3 movementDirection;
    [SerializeField] private Vector3 movementDebug;
    [SerializeField] public bool isAttacking;


    public void SetupBehaviour()
    {
        SetGameplayCamera();
    }

    void SetGameplayCamera()
    {
        //mainCamera = CameraManager.Instance.GetGameplayCamera();
    }

    public void UpdateMovementData(Vector3 newMovementDirection)
    {
        movementDirection = newMovementDirection;
    }

    void FixedUpdate()
    {
        if (isAttacking == false)
        {
            MoveThePlayer();
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

        playerRigidbody.MovePosition(transform.position + movement);
    }

    void AimTowardsMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

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

            Quaternion rotation = Quaternion.Slerp(playerRigidbody.rotation,
                                                 Quaternion.LookRotation(CameraDirection(movementDirection)),
                                                 turnSpeed);

            playerRigidbody.MoveRotation(rotation);

        }
    }


    Vector3 CameraDirection(Vector3 movementDirection)
    {
        var cameraForward = mainCamera.transform.forward;
        var cameraRight = mainCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        return cameraForward * movementDirection.z + cameraRight * movementDirection.x;

    }

}