using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region // Singleton Instance
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(this);
        }
    }
    #endregion // Singleton Instance

    [Header("Components")]
    [SerializeField] private TutorialManager _tutorialManager;
    [SerializeField] private PlayerController _playerController;

    [Header("Information")]
    [SerializeField] private bool _isTutorial = false;

    public bool IsTutorial => _isTutorial;
    public PlayerController Player => _playerController;
    public TutorialManager TutorialManager => _tutorialManager;

    private void Start()
    {
        if (IsTutorial)
        {
            _tutorialManager = GameObject.FindWithTag("Tutorial").GetComponent<TutorialManager>();
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
}
