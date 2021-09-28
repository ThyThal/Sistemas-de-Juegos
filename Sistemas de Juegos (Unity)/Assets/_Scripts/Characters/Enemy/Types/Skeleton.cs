using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Skeleton : Enemy, IAttacker
{
    [Header("Skeleton Information")]
    [SerializeField] private NavMeshAgent _enemyAgent;
    [SerializeField] private bool _isActivated = false;
    [SerializeField] private EnemyStats _enemyStats;
    [SerializeField] private int _damage;
    [SerializeField] private int _speed;
    [SerializeField] private PlayerController _player;

    [Header("Enemy Ranges")]
    [SerializeField] private float _playerSeekRange = 10f;
    [SerializeField] private float _playerMovementCooldown = 3.5f;

    [Header("Enemy Cooldowns")]
    private bool canMove = true;

    [Header("Attack Information")]
    [SerializeField] private float _playerAttackRange = 2.25f;
    [SerializeField] private Vector2 _attackCooldown = new Vector2(5f, 11f);
    [SerializeField] private float _attackTimer = 0f;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        CharacterData(_enemyStats.Health);
        _damage = _enemyStats.Damage;
        _speed = _enemyStats.Speed;
        _player = GameManager.Instance.Player as PlayerController;
    }

    private void Update()
    {
        _animator.SetFloat("Velocity", 0);
        AttackMovementCooldown();

        if (_attackTimer >= 0) { _attackTimer -= Time.deltaTime; }

        if (_isActivated)
        {
            var playerDistance = Vector3.Distance(_player.transform.position, transform.position);

            if (playerDistance <= _playerSeekRange && canMove == true)
            {
                MoveTowardsPlayer();
            }

            if (_attackTimer <= 0)
            {

                if (playerDistance <= _playerAttackRange)
                {
                    Attack();
                }
            }            
        }
    }




    private void MoveTowardsPlayer()
    {
        _enemyAgent.SetDestination(_player.transform.position);
        _animator.SetFloat("Velocity", _enemyAgent.velocity.magnitude);
    } // Move To Player
    public void Attack()
    {
        canMove = false;
        _attackTimer = Random.Range(_attackCooldown.x, _attackCooldown.y);
        _animator.SetTrigger("Attack");
    } // Do Attack
    public void OnAttack()
    {
        var playerDistance = Vector3.Distance(_player.transform.position, transform.position);
        if (playerDistance <= _playerAttackRange + 0.5f)
        {
            GameManager.Instance.Player.TakeDamage(_damage);
        }
        
    } // Make Damage

    private void AttackMovementCooldown() // Control Attack Cooldown
    {
        if (canMove == false)
        {
            _playerMovementCooldown -= Time.deltaTime;

            if (_playerMovementCooldown <= 0)
            {
                canMove = true;
                _playerMovementCooldown = 3.5f;
            }
        }
    }


    // DEBUG METHODS
    [ContextMenu("Toggle Enemy")]
    private void ToggleEnemy()
    {
        if (_isActivated) { _isActivated = false; }
        else { _isActivated = true; }
    }
}
