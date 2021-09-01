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

    private float _movementSmoothingSpeed = 0.75f;
    private float _shootCooldownOG;
    [SerializeField] private bool _isAttacking = false;

    [SerializeField] private float _shootCooldown = 1f;
    [SerializeField] private LootableReward _lootable;
    [SerializeField] private Vector3 _rawInputMovement;
    [SerializeField] private Vector3 _smoothInputMovement;
    [SerializeField] private AudioClip[] _attackClips;
    [SerializeField] private AudioSource _attackSource;

    public Transform ShootPoint => _shootPoint;
    public PlayerInventory PlayerInventory => _playerInventory;
    public LootableReward LootableReward => _lootable;


    // MonoBehaviours
    private void Start()
    {
        _shootCooldownOG = _shootCooldown;
    }

    private void Update()
    {
        if (_shootCooldown > 0) { _shootCooldown -= Time.deltaTime; } // Shoot Cooldown
        if (_shootCooldown <= 0) { _isAttacking = false; }
        
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
        if (value.started && _shootCooldown <= 0)
        {
            _isAttacking = true;
            _shootCooldown = _shootCooldownOG;
            _attackSource.PlayOneShot(_attackClips[Random.Range(0, _attackClips.Length)]);
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
