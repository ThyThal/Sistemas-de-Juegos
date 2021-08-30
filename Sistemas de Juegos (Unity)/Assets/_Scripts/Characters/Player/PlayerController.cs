using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerController : Character, IPlayerInput
{
    [Header("Controllers")]
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAnimations _playerAnimations;
    [SerializeField] private Transform _shootPoint;

    private float _movementSmoothingSpeed = 1f;
    [SerializeField] private LootableReward _lootable;
    [SerializeField] private Vector3 _rawInputMovement;
    [SerializeField] private Vector3 _smoothInputMovement;

    public Transform ShootPoint => _shootPoint;
    public PlayerInventory PlayerInventory => _playerInventory;
    public LootableReward LootableReward => _lootable;

    // MonoBehaviours
    private void Update()
    {
        //_shootPoint.localRotation = transform.localRotation;

        UpdatePlayerMovement();
        UpdatePlayerAnimations();
    }

    // Methods
    private void UpdatePlayerMovement()
    {
        // Disable Smooth Movement.
        //_smoothInputMovement = Vector3.Lerp(_smoothInputMovement, _rawInputMovement, Time.deltaTime * _movementSmoothingSpeed);
        _playerMovement.UpdateMovementData(_rawInputMovement);
    }

    private void UpdatePlayerAnimations()
    {
        _playerAnimations.UpdateMovementAnimation(_rawInputMovement.magnitude);
    }

    public void AddReward(LootableReward reward)
    {
        _lootable = reward;
    }

    public void RemoveReward()
    {
        _lootable = null;
    }

    // IPlayerInput System Events
    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        _rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            _playerAnimations.PlayAttackAnimation();
        }
    }

    public void OnLoot(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            if (_lootable != null)
            {
                _lootable.GrabLootable();
            }
        }
    }
}
