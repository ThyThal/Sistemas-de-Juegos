using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWindow : MonoBehaviour
{
    [Header("Tutorial Window")]
    [SerializeField] private Animator _animator;
    [SerializeField] public bool HiddenTutorial;
    [SerializeField] public bool FinishedTutorial;
    [SerializeField] public Color ColorComplete;
    public Animator Animator => _animator;
}
