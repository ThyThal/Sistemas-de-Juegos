using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    private void Start()
    {
        _tutorialManager = GameObject.FindWithTag("Tutorial").GetComponent<TutorialManager>();
    }

    [SerializeField] private TutorialManager _tutorialManager;
    [SerializeField] private PlayerController _playerController;
    public PlayerController Player => _playerController;
    public TutorialManager TutorialManager => _tutorialManager;
}
