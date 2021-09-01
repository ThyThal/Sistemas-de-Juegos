using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    [Header("Tutorial Managers")]
    [SerializeField] private TutorialMovement _tutorialMovement;
    [SerializeField] private TutorialAttack _tutorialAttack;
    [SerializeField] private TutorialLoot _tutorialLoot;

    [Header("Tutorial UI Windows")]
    [SerializeField] private int _tutorialIndex;
    [SerializeField] private GameObject[] _tutorialWindows;

    // GameManager Player
    private PlayerInput _playerInput;
    private InputAction _attackAction;
    private InputAction _lootAction;

    // Properties
    public int CurrentIndex => _tutorialIndex;
    public PlayerInput PlayerInput => _playerInput;
    public TutorialMovement TutorialMovement => _tutorialMovement;
    public TutorialAttack TutorialAttack => _tutorialAttack;
    public TutorialLoot TutorialLoot => _tutorialLoot;


    private void Start()
    {
        _playerInput = GameManager.Instance.Player.GetComponent<PlayerInput>();
        _attackAction = _playerInput.actions.FindAction("Attack");
        _attackAction.Disable();
        _lootAction = _playerInput.actions.FindAction("Loot");
        _lootAction.Disable();
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

    public IEnumerator HideTutorial()
    {
        var currentTutorial = _tutorialWindows[_tutorialIndex].GetComponent<TutorialWindow>();

        // Play UI Animation.
        yield return new WaitForSeconds(1f);
        currentTutorial.Animator.SetTrigger("Finished");
        yield return new WaitForSeconds(1f);

        // Change Tutorial Index
        if (currentTutorial.HiddenTutorial && currentTutorial.FinishedTutorial)
        {
            _tutorialIndex++;
        }
    }
    public void FinishedTutorial()
    {
        var currentTutorial = _tutorialWindows[_tutorialIndex].GetComponent<TutorialWindow>();
        if (currentTutorial.HiddenTutorial == false)
        {
            currentTutorial.HiddenTutorial = true;
            currentTutorial.FinishedTutorial = true;
            StartCoroutine(HideTutorial());
        }

        else
        {
            _tutorialIndex++;
        }
    }

    public void InputToggleAttack(bool value)
    {
        if (value == true) { _attackAction.Enable(); }
        else { _attackAction.Disable(); }
    }
    public void InputToggleLoot(bool value)
    {
        if (value == true) { _lootAction.Enable(); }
        else { _lootAction.Disable(); }
    }
}
