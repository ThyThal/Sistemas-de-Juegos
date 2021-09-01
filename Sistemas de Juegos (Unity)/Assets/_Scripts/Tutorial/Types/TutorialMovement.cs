using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TutorialMovement : TutorialWindow, ITutorial
{
    [Header("Keys Pressed")]
    [SerializeField] private bool _up = false;
    [SerializeField] private bool _down = false;
    [SerializeField] private bool _left = false;
    [SerializeField] private bool _right = false;

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

    // Interface Tutorial
    public IEnumerator TutorialStart()
    {
        yield return new WaitForSeconds(1f);
        Animator.SetTrigger("Start");
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
        if (_left && _right && _up && _down && !FinishedTutorial)
        {
            Debug.Log("[===] Finished Movement Tutorial! [===]");
            TutorialFinish();
        }
    }

    // Player Input Events
    public void OnMovementTutorial(InputAction.CallbackContext value)
    {
        if ((value.control.name == "w" || value.control.name == "upArrow") && !_up)
        {
            _up = true;
            _upArrow.color = ColorComplete;
            _upLetter.color = ColorComplete;
        }

        if ((value.control.name == "s" || value.control.name == "downArrow") && !_down)
        {
            _down = true;
            _downArrow.color = ColorComplete;
            _downLetter.color = ColorComplete;
        }

        if ((value.control.name == "a" || value.control.name == "leftArrow") && !_left)
        {
            _left = true;
            _leftArrow.color = ColorComplete;
            _leftLetter.color = ColorComplete;
        }

        if ((value.control.name == "d" || value.control.name == "rightArrow") && !_right)
        {
            _right = true;
            _rightArrow.color = ColorComplete;
            _rightLetter.color = ColorComplete;
        }
    }
}
