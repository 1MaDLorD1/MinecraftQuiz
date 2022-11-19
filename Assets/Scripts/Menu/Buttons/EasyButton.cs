using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EasyButton : DifficultyButton
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
        _winScore.ScoreAdded += OnScoreAdded;
    }

    private void OnScoreAdded()
    {
        _isStarted = false;
        _winScore.ScoreAdded -= OnScoreAdded;
    }
}
