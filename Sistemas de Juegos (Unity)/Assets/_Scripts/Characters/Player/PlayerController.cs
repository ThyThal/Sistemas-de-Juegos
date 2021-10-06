using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Character
{
    [Header("Player Components")]
    [SerializeField] private GameObject _defaultWeapon;
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAnimations _playerAnimations;

    [Header("Shooting Information")]
    [SerializeField] private GameObject _currentProjectile;
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private float _shootCooldown = 1f;
    [SerializeField] private bool _isAttacking = false;
    private float _shootCooldownOG;

    [Header("Movement Information")]
    [SerializeField] private Vector3 _rawInputMovement;

    [Header("Lootable Rewards")]
    [SerializeField] private LootableReward _lootableReward;

    private AttackCommand _attackCommand;


    private void Awake()
    {
        GameManager.Instance.Player = this;
    }

    //======================[ PROPERTIES ]======================\\

    // Player Components.
    public PlayerInventory PlayerInventory => _playerInventory;
    public GameObject CurrentProjectile => _playerInventory.CurrentWeapon.WeaponStats.WeaponSpellPrefab;

    // Shooting Information.
    public Transform ShootingPoint => _shootingPoint;

    //======================[ UNITY MONO BEHAVIOURS ]======================\\
    private void Start()
    {
        Heal(float.PositiveInfinity);
        InitializeCommands();


        if (_defaultWeapon != null)
        {
            var defaultWeapon = Instantiate(_defaultWeapon, transform);
            _playerInventory.LootWeapons(defaultWeapon);
        }

        _shootCooldownOG = _shootCooldown;
    }

    private void Update()
    {
        if (_shootCooldown > 0) { _shootCooldown -= Time.deltaTime; } // Shoot Cooldown
        if (_shootCooldown <= 0) { _isAttacking = false; _playerMovement.isAttacking = false; }

        //_shootPoint.localRotation = transform.localRotation;

        if (_isAttacking == false)
        {
            UpdatePlayerMovement();
            UpdatePlayerAnimations();
        }

    }


    //======================[ METHODS ]======================\\
    public void AddReward(LootableReward reward)
    {
        _lootableReward = reward;
    }

    public void RemoveReward()
    {
        _lootableReward = null;
    }

    private void UpdatePlayerMovement()
    {
        _playerMovement.UpdateMovementData(_rawInputMovement);
    }

    private void UpdatePlayerAnimations()
    {
        _playerAnimations.UpdateMovementAnimation(_rawInputMovement.magnitude);
    }

    //======================[ PLAYER INPUT AVTIONS ]======================\\
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
            _playerMovement.isAttacking = true;
            _shootCooldown = _shootCooldownOG;
            //_attackSource.PlayOneShot(_attackClips[Random.Range(0, _attackClips.Length)]);
            _playerAnimations.PlayAttackAnimation();
        }
    }

    public void OnLoot(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            if (_lootableReward != null && _lootableReward.Spawned)
            {
                _lootableReward.GrabLootable();
            }
        }
    }

    /////////////
    private void InitializeCommands()
    {
        _attackCommand = new AttackCommand(_playerInventory.CurrentWeapon);
    }
}
