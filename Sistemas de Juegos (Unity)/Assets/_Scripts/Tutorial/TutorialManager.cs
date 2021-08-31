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
    [SerializeField] private Transform[] _enemySpawns;
    [SerializeField] private GameObject[] _tutorialWindows;
    [SerializeField] private int _tutorialIndex;
    [SerializeField] private Color _colorComplete;
    [SerializeField] private int _pressedKeys;

    [Header("Completed Tutorials")]
    [SerializeField] private bool _movementTutorialFinished = false;
    [SerializeField] private bool _attackTutorialFinished = false;

    [Header("Keys Pressed")]
    [SerializeField] private bool _up = false;
    [SerializeField] private bool _down = false;
    [SerializeField] private bool _left = false;
    [SerializeField] private bool _right = false;
    [SerializeField] private bool _attack = false;


    [Header("Tutorial Movement Letters")]
    [SerializeField] private Image _upLetter;
    [SerializeField] private Image _downLetter;
    [SerializeField] private Image _leftLetter;
    [SerializeField] private Image _rightLetter;

    [Header("Tutorial Movement Arrows")]
    [SerializeField] private Image _upArrow;
    [SerializeField] private Image _downArrow;
    [SerializeField] private Image _leftArrow;
    [SerializeField] private Image _rightArrow;

    [Header("Tutorial Attack")]
    [SerializeField] private Image _attackKeyboard;
    [SerializeField] private Image _attackMouse;

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

            _tutorialIndex++;
        }
    }
}
