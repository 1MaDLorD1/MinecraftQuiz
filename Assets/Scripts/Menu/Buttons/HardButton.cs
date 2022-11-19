using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HardButton : DifficultyButton
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private WinScore _winScore;

    private bool _isStarted;
    public bool IsStarted => _isStarted;

    protected override void HandleClickButton()
    {
        _isStarted = true;
        ButtonClicked?.Invoke(_gameSettings);
        base.HandleClickButton(); 
    }

    private void OnEnable()
    {
        _menu.HardLevelPassed += OnMediumLevelPassed;
        _winScore.ScoreAdded += OnScoreAdded;
    }

    private void OnDisable()
    {
        _menu.HardLevelPassed -= OnMediumLevelPassed;
    }

    private void OnMediumLevelPassed()
    {
        _button.onClick.AddListener(HandleClickButton);
    }

    private void OnScoreAdded()
    {
        _isStarted = false;
        _winScore.ScoreAdded -= OnScoreAdded;
    }
}
