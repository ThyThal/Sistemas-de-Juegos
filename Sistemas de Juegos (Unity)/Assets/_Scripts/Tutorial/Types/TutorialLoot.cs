using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TutorialLoot : TutorialWindow, ITutorial
{
    [Header("Helpers")]
    [SerializeField] private GameObject _epicReward;
    [SerializeField] private GameObject _rareReward;
    [SerializeField] private GameObject _playerSmallWalls;
    [SerializeField] private GameObject _playerBigWalls;
    [SerializeField] private Transform _rarePlatform;
    [SerializeField] private Transform _epicPlatform;

    [Header("Loot")]
    [SerializeField] private bool _loot = false;
    [SerializeField] private int _grabbedItems = 0;
    [SerializeField] private Image _lootKey;
    [SerializeField] private Image _lootDescription;

    [Header("Enemies")]
    [SerializeField] private GameObject _currentEnemy;
    [SerializeField] private GameObject _enemyDummy;
    [SerializeField] private Transform _enemySpawn;
    [SerializeField] private int _tutorialKilled = 0;

    private bool _secondLoot = false;



    // Interface Tutorial
    public IEnumerator TutorialStart()
    {
        Animator.SetTrigger("Start");

        // Spawn Rewards
        yield return new WaitForSeconds(1f);
        _epicReward.SetActive(true);
        _rareReward.SetActive(true);
        yield return new WaitForSeconds(1f);

        // Move Platforms
        _epicPlatform.gameObject.SetActive(true);
        _epicPlatform.DOMove(new Vector3(_epicPlatform.transform.position.x, 0, _epicPlatform.transform.position.z), 1);
        _rarePlatform.gameObject.SetActive(true);
        _rarePlatform.DOMove(new Vector3(_rarePlatform.transform.position.x, 0, _rarePlatform.transform.position.z), 1);
        yield return new WaitForSeconds(1f);

        _playerBigWalls.SetActive(true);
        _playerSmallWalls.SetActive(false);

        GameManager.Instance.TutorialManager.InputToggleLoot(true);
        yield return null;
    }
    public void TutorialFinish()
    {
        FinishedTutorial = true;
        GameManager.Instance.TutorialManager.FinishedTutorial();
    }

    // MonoBehaviours
    private void Start()
    {
        StartCoroutine(TutorialStart());
    }
    private void Update()
    {
        if (_grabbedItems == 2 && _tutorialKilled == 2)
        {
            Debug.Log("[===] Finished Looting Tutorial! [===]");
            TutorialFinish();
        }
    }

    // Player Input Events
    public void OnLootTutorial(InputAction.CallbackContext value)
    {
        if (value.control.name == "e")
        {
            if (!_loot)
            {
                _loot = true;
                _lootKey.color = ColorComplete;
                _lootDescription.color = ColorComplete;
                HiddenTutorial = true;
                StartCoroutine(GameManager.Instance.TutorialManager.HideTutorial());
                FirstGrabbedItem();
            }

            if (_loot && _currentEnemy == null && _tutorialKilled == 1) // Second Enemy
            {
                SpawnRewardEnemy();
            }

            if (_loot && _currentEnemy != null && _grabbedItems == 2 && !_secondLoot && _tutorialKilled == 0) // First Enemy No Kill.
            {
                _secondLoot = true;
                TutorialAddKill();
            }
        }
    }

    private void SpawnRewardEnemy()
    {
        var dummy = Instantiate(_enemyDummy, _enemySpawn);
        dummy.transform.SetParent(_enemySpawn);
        dummy.transform.LookAt(GameManager.Instance.Player.transform);
        _currentEnemy = dummy.gameObject;
    }

    public void TutorialAddKill()
    {
        _tutorialKilled++;
    }
    public void AddGrabbed()
    {
        _grabbedItems++;
    }


    private void FirstGrabbedItem()
    {
        SpawnRewardEnemy();
    }
}

