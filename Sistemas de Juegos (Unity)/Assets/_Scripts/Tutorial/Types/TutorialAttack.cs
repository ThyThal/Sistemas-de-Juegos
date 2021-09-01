using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialAttack : TutorialWindow, ITutorial
{
    [Header("Tutorial Attack")]
    [SerializeField] private bool _attack = false;
    [SerializeField] private Image _attackKeyboard;
    [SerializeField] private Image _attackMouse;

    [Header("Enemy Information")]
    [SerializeField] private GameObject _enemyDummy;
    [SerializeField] private Transform _enemyPlatforms;
    [SerializeField] private Transform[] _enemySpawns;

    [Header("Extras")]
    [SerializeField] private int _tutorialKilled = 0;

    // Interface Tutorial
    public IEnumerator TutorialStart()
    {
        Animator.SetTrigger("Start");

        // Move Enemy Platforms.
        _enemyPlatforms.DOMove(new Vector3(0, 0, 0), 1f);
        yield return new WaitForSeconds(1f);

        // Spawn Enemies in Each Platform.
        foreach (var spawn in _enemySpawns)
        {
            var dummy = Instantiate(_enemyDummy, spawn);
            dummy.transform.SetParent(spawn);
            dummy.transform.LookAt(GameManager.Instance.Player.transform);
        }

        // Enable Attack Input.
        GameManager.Instance.TutorialManager.InputToggleAttack(true);
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
        if (_attack && !HiddenTutorial)
        {
            HiddenTutorial = true;
            StartCoroutine(GameManager.Instance.TutorialManager.HideTutorial());
        }

        if (_attack && !FinishedTutorial && _tutorialKilled >= 3)
        {
            Debug.Log("[===] Finished Attack Tutorial! [===]");
            TutorialFinish();
        }
    }

    // Player Input Events
    public void OnAttackTutorial(InputAction.CallbackContext value)
    {
        if ((value.control.name == "space" || value.control.name == "leftButton") && !_attack)
        {
            _attack = true;
            _attackKeyboard.color = ColorComplete;
            _attackMouse.color = ColorComplete;
        }
    }

    public void TutorialAddKill()
    {
        _tutorialKilled++;
    }
}
