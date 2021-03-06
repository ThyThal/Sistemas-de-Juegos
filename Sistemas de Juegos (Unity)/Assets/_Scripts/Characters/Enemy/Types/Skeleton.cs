using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Skeleton : Enemy, IAttacker
{
    [Header("Skeleton Information")]
    [SerializeField] private Vector2 idleTimer= new Vector2(3f, 7f);
    private float idleTime;
    [SerializeField] private NavMeshAgent _enemyAgent;
    [SerializeField] private bool _isActivated = false;
    [SerializeField] private EnemyStats _enemyStats;
    [SerializeField] private int _damage;
    [SerializeField] private int _speed;
    [SerializeField] private PlayerController _player;

    [Header("Enemy Ranges")]
    [SerializeField] private SphereCollider _rangeCollider;
    [SerializeField] private float _playerSeekRange = 10f;
    [SerializeField] private float _playerMovementCooldown = 3.5f;

    [Header("Enemy Cooldowns")]
    private bool canMove = true;

    [Header("Attack Information")]
    [SerializeField] private bool _isAngry = false;
    [SerializeField] private bool _isAttacking = false;
    [SerializeField] private float _playerAttackRange = 2.25f;
    [SerializeField] private Vector2 _attackCooldown = new Vector2(5f, 11f);
    [SerializeField] private float _attackTimer = 0f;
    [SerializeField] private float playerDistance;

    private void Start()
    {
        CharacterData(_enemyStats.Health);
        _damage = _enemyStats.Damage;
        _speed = _enemyStats.Speed;
        _player = GameManager.Instance.Player;
        _rangeCollider.gameObject.transform.localScale = Vector3.one * _playerSeekRange * 2;
        idleTime = Random.Range(idleTimer.x, idleTimer.y);
    }

    private void Update()
    {
        Animator.SetFloat("Velocity", 0);
        AttackMovementCooldown();

        if (_isActivated)
        {
            PlayIdleSound();
        }

        if (_attackTimer >= 0) { _attackTimer -= Time.deltaTime; }

        if (_isActivated && CurrentHealth > 0)
        {
            playerDistance = Vector3.Distance(_player.transform.position, transform.position);

            if (!_isAttacking) // Can Move
            {
                if (playerDistance <= _playerSeekRange && canMove && !_isAngry)
                {
                    MoveTowardsPlayer();
                }

                else if (_isAngry && canMove)
                {
                    MoveTowardsPlayer();
                }
            }

            CheckAttack();
        }
    }

    private void CheckAttack()
    {
        if (_attackTimer <= 0)
        {

            if (playerDistance <= _playerAttackRange)
            {
                Attack();
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        _enemyAgent.SetDestination(_player.transform.position);
        Animator.SetFloat("Velocity", _enemyAgent.velocity.magnitude);
    } // Move To Player
    public void Attack()
    {
        canMove = false;
        _attackTimer = Random.Range(_attackCooldown.x, _attackCooldown.y);
        Animator.SetTrigger("Attack");
    } // Do Attack
    public void OnAttack()
    {
        CharacterAudio.AudioAttack.PlayAudio();
        var playerDistance = Vector3.Distance(_player.transform.position, transform.position);
        if (playerDistance <= _playerAttackRange + 0.5f)
        {
            GameManager.Instance.Player.TakeDamage(_damage);
        }

        _isAttacking = false;
        canMove = true;
        
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

    public void OnSkeletonDie()
    {
        _rangeCollider.gameObject.SetActive(false);
        _enemyAgent.enabled = false;
        canMove = false;
        _isAngry = false;
        _isActivated = false;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        CharacterAudio.AudioHurt.PlayAudio();
        idleTime = Random.Range(idleTimer.x, idleTimer.y);

        if (!_isAngry)
        {
            _isAngry = true;
        }
    }

    private void PlayIdleSound()
    {
        if (idleTime > 0)
        {
            idleTime -= Time.deltaTime;
        }

        else
        {
            idleTime = Random.Range(idleTimer.x, idleTimer.y);
            CharacterAudio.AudioIdle.PlayAudio();
        }
    }
}
