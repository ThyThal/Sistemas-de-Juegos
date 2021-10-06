using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Game Manager Instance
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    [Header("Components")]
    [SerializeField] private TutorialManager _tutorialManager;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Pool _playerBulletsPool;
    [SerializeField] private Pool _bossBulletsPool;
    [SerializeField] private Pool _enemiesPool;
    

    [Header("Information")]
    [SerializeField] private bool _isTutorial = false;
    [SerializeField] private bool _hasWon = false;
    public bool firstGame = true;
    public int SpawnersKilled;


    public bool IsTutorial => _isTutorial;
    public PlayerController Player
    {
        get { return _playerController; }
        set { _playerController = value; }
    }
    public TutorialManager TutorialManager => _tutorialManager;
    public Pool PlayerBulletsPool => _playerBulletsPool;
    public Pool BossBulletsPool => _bossBulletsPool;
    public Pool EnemiesPool => _enemiesPool;
    public bool HasWon => _hasWon;

    private void Start()
    {
        if (IsTutorial)
        {
            _tutorialManager = GameObject.FindWithTag("Tutorial").GetComponent<TutorialManager>();
        }
    }

    private void Update()
    {
        if (_playerController != null)
        {
            if (_playerController.CurrentHealth <= 0)
            {
                GameWinCondition(false);
            }

            if (SpawnersKilled >= 3)
            {
                _hasWon = true;
                GameWinCondition(true);
            }
        }
    }

    public void StartTutorial()
    {
        _isTutorial = true;
    }

    public void FinishTutorial()
    {
        _isTutorial = false;
    }

    public void KilledEnemy()
    {
        if (IsTutorial)
        {
            if (TutorialManager.CurrentIndex == 1)
            {
                TutorialManager.TutorialAttack.TutorialAddKill();
            }

            if (TutorialManager.CurrentIndex == 2)
            {
                TutorialManager.TutorialLoot.TutorialAddKill();
            }
        }

        else
        {
            // Add Killed Enemy
        }
    }

    public void GameWinCondition(bool condition)
    {
        if (condition)
        {
            SceneManager.LoadScene(0);
        }

        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
