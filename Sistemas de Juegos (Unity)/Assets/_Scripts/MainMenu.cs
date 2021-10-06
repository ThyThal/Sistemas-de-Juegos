using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _conditionObject;
    [SerializeField] private Text _conditionText;
    [SerializeField] private Color _winColor;
    [SerializeField] private Color _loseColor;

    private void Start()
    {
        OnCondition();
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene(1);
        GameManager.Instance.firstGame = false;
    }

    public void OnClickExit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void OnCondition()
    {
        if (GameManager.Instance.firstGame == false)
        {
            _conditionObject.SetActive(true);
            _conditionText.text = "";

            if (GameManager.Instance.HasWon)
            {
                _conditionText.color = _winColor;
                _conditionText.text = "You Win!";
            }

            else
            {
                _conditionText.color = _loseColor;
                _conditionText.text = "Game Over!";
            }
        }
    }
}
