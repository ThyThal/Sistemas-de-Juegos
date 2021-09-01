using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyDummy;
    [SerializeField] private Transform _enemyPlataforms;
    [SerializeField] private Transform _middlePlatform;
    [SerializeField] private Transform[] _enemySpawns;
    [SerializeField] private GameObject[] _tutorialWindows;
    [SerializeField] private int _tutorialIndex;
    [SerializeField] private Color _colorComplete;
    [SerializeField] private int _pressedKeys;
    [SerializeField] private int _killedTutorialAmount = 0;
    [SerializeField] private bool _killedEnemies;
    [SerializeField] public int _grabbedItem;
    [SerializeField] private bool _finishedRewards = false;

    [Header("Completed Tutorials")]
    [SerializeField] private bool _movementTutorialFinished = false;
    [SerializeField] private bool _attackTutorialFinished = false;

    [Header("Keys Pressed")]
    [SerializeField] private bool _up = false;
    [SerializeField] private bool _down = false;
    [SerializeField] private bool _left = false;
    [SerializeField] private bool _right = false;
    [SerializeField] private bool _attack = false;
    [SerializeField] private bool _loot = false;

    [Header("Tutorial Movement Letters")]
    [SerializeField] private Image _upLetter;
    [SerializeField] private Image _downLetter;
    [SerializeField] private Image _leftLetter;
    [SerializeField] private Image _rightLetter;
    [SerializeField] private Image _lootLetter;
    [SerializeField] private Image _lootDescription;

    [Header("Tutorial Movement Arrows")]
    [SerializeField] private Image _upArrow;
    [SerializeField] private Image _downArrow;
    [SerializeField] private Image _leftArrow;
    [SerializeField] private Image _rightArrow;

    [Header("Tutorial Attack")]
    [SerializeField] private Image _attackKeyboard;
    [SerializeField] private Image _attackMouse;

    [Header("Tutorial Rewards")]
    [SerializeField] private GameObject _oldWall;
    [SerializeField] private GameObject _newWall;
    [SerializeField] private GameObject _rewardEpic;
    [SerializeField] private Transform _rewardEpicPlatform;
    [SerializeField] private GameObject _rewardRare;
    [SerializeField] private Transform _rewardRarePlatform;

    private void Start()
    {
        GameManager.Instance.Player.GetComponent<PlayerInput>().actions.FindAction("Attack").Disable();
    }

    private void Update()
    {
        for (int i = 0; i < _tutorialWindows.Length; i++)
        {
            if (i == _tutorialIndex)
            {
                _tutorialWindows[i].SetActive(true);
            }

            else
            {
                _tutorialWindows[i].SetActive(false);
            }
        }

        if (_killedTutorialAmount >= 3 && !_killedEnemies)
        {
            _killedEnemies = true;
            _tutorialIndex++;
            StartCoroutine(TutorialStartRewards());
        }

        if (_killedTutorialAmount >= 2 && _attackTutorialFinished && _loot && _tutorialIndex == 2)
        {
            TutorialFinishLooting();
        }
    }



    // Tutorial Movement Input.
    public void TutorialStartInput()
    {

    }

    public void TutorialReadMovementInput(InputAction.CallbackContext value)
    {
        if ((value.control.name == "w" || value.control.name == "upArrow") && !_up)
        {
            _up = true;
            _upArrow.color = _colorComplete;
            _upLetter.color = _colorComplete;
            _pressedKeys++;
        }

        if ((value.control.name == "s" || value.control.name == "downArrow") && !_down)
        {
            _down = true;
            _downArrow.color = _colorComplete;
            _downLetter.color = _colorComplete;
            _pressedKeys++;
        }

        if ((value.control.name == "a" || value.control.name == "leftArrow") && !_left)
        {
            _left = true;
            _leftArrow.color = _colorComplete;
            _leftLetter.color = _colorComplete;
            _pressedKeys++;
        }

        if ((value.control.name == "d" || value.control.name == "rightArrow") && !_right)
        {
            _right = true;
            _rightArrow.color = _colorComplete;
            _rightLetter.color = _colorComplete;
            _pressedKeys++;
        }

        if (_pressedKeys >= 4 && _tutorialIndex == 0)
        {
            StartCoroutine(TutorialFinishMovement());
        }
    }

    public void TutorialReadAttackInput(InputAction.CallbackContext value)
    {
        if ((value.control.name == "space" || value.control.name == "leftButton") && !_attack && _tutorialIndex == 1)
        {
            _attack = true;
            _attackKeyboard.color = _colorComplete;
            _attackMouse.color = _colorComplete;
            _pressedKeys++;
        }

        if (_pressedKeys >= 5)
        {
            StartCoroutine(TutorialFinishAttack());
        }
    }

    public IEnumerator TutorialFinishMovement()
    {
        if (!_movementTutorialFinished)
        {
            _movementTutorialFinished = true;

            // Animation
            yield return new WaitForSeconds(1f);
            _tutorialWindows[_tutorialIndex].GetComponent<Animator>()?.SetTrigger("Finished");
            yield return new WaitForSeconds(1f);

            // Move Plataforms
            _enemyPlataforms.DOMove(new Vector3(0, 0, 0), 1);

            yield return new WaitForSeconds(1f);
            foreach (var spawner in _enemySpawns)
            {
                var dummy = Instantiate(_enemyDummy, spawner);
                dummy.transform.SetParent(spawner);
                dummy.transform.LookAt(GameManager.Instance.Player.transform);
            }

            GameManager.Instance.Player.GetComponent<PlayerInput>().actions.FindAction("Attack").Enable();
            _tutorialIndex++;
        }
    }

    public IEnumerator TutorialFinishAttack()
    {
        if (!_attackTutorialFinished)
        {
            _attackTutorialFinished = true;

            // Animation
            yield return new WaitForSeconds(1f);
            _tutorialWindows[_tutorialIndex].GetComponent<Animator>()?.SetTrigger("Finished");
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator TutorialStartRewards()
    {
        _killedTutorialAmount = 0;

        // Remove Middle Platform
        //_middlePlatform.DOMove(new Vector3(_middlePlatform.transform.position.x, -1, _middlePlatform.transform.position.z), 2);
        yield return new WaitForSeconds(1f);
        _rewardEpic.SetActive(true);
        _rewardRare.SetActive(true);
        yield return new WaitForSeconds(1f);

        // Spawn Rewards
        _rewardEpicPlatform.gameObject.SetActive(true);
        _rewardEpicPlatform.DOMove(new Vector3(_rewardEpicPlatform.transform.position.x, 0, _rewardEpicPlatform.transform.position.z), 1);
        _rewardRarePlatform.gameObject.SetActive(true);
        _rewardRarePlatform.DOMove(new Vector3(_rewardRarePlatform.transform.position.x, 0, _rewardRarePlatform.transform.position.z), 1);
        yield return new WaitForSeconds(1.5f);


        _newWall.SetActive(true);
        _oldWall.SetActive(false);
    }

    public void AddTutorialKilled()
    {
        _killedTutorialAmount++;
    }

    public void TutorialReadLootingInput(InputAction.CallbackContext value)
    {
        if ((value.control.name == "e") && !_loot && _tutorialIndex == 2)
        {
            if (_grabbedItem == 1)
            {
                _loot = true;
                _lootLetter.color = _colorComplete;
                _lootDescription.color = _colorComplete;
                StartCoroutine(TutorialDelayLoot());
                StartCoroutine(SpawnRewardEnemy());
                _pressedKeys++;
            }
        }

        if (_pressedKeys >= 6 && _grabbedItem == 2 && _finishedRewards == false)
        {
            _finishedRewards = true;
            StartCoroutine(SpawnRewardEnemy());
        }
    }


    private IEnumerator TutorialDelayLoot()
    {
        yield return new WaitForSeconds(1f);
        _tutorialWindows[_tutorialIndex].GetComponent<Animator>()?.SetTrigger("Finished");
    }
    private void TutorialFinishLooting()
    {
        _finishedRewards = true;

        if (_killedTutorialAmount >= 2 && _finishedRewards == true)
        {
            _tutorialIndex++;
            Debug.Log("Finished Tutorial!");
        }

    }

    private IEnumerator SpawnRewardEnemy()
    {
        var dummy = Instantiate(_enemyDummy, _enemySpawns[0]);
        dummy.transform.SetParent(_enemySpawns[0]);
        dummy.transform.LookAt(GameManager.Instance.Player.transform);
        yield return new WaitForSeconds(1f);
    }
}

