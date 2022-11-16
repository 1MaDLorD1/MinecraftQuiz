using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class DifficultyButton : MonoBehaviour
{
    [SerializeField] private string _difficulty;
    [SerializeField] private TMP_Text _levelDifficulty;
    [SerializeField] protected bool _isUnlocked;
    [SerializeField] protected Game _game;
    [SerializeField] protected Menu _menu;

    private Button _button;

    public UnityAction<GameSettings> ButtonClicked;

    private void Start()
    {
        if (_button == null) _button = GetComponent<Button>();

        _levelDifficulty.text = _difficulty.ToString();
        if(_isUnlocked)
            _button.onClick.AddListener(HandleClickButton);
    }

    protected virtual void HandleClickButton()
    {
        if (_isUnlocked)
        {
            _game.gameObject.SetActive(true);
            _menu.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _game.LevelComplete += OnLevelComplete;
    }

    private void OnDisable()
    {
        _game.LevelComplete -= OnLevelComplete;
    }

    private void OnLevelComplete()
    {
        if (!_isUnlocked)
            _button.onClick.AddListener(HandleClickButton);
        _isUnlocked = true;
        _levelDifficulty.color = Color.white;
    }
}
