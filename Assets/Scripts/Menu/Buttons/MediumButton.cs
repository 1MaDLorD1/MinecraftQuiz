using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MediumButton : DifficultyButton
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private WinScore _winScore;

    private bool _isStarted;
    public bool IsStarted => _isStarted;

    public bool IsPassed { get; set; }

    public UnityAction MediumLevelStarted;

    protected override void HandleClickButton()
    {
        _isStarted = true;
        MediumLevelStarted?.Invoke();
        ButtonClicked?.Invoke(_gameSettings);
        base.HandleClickButton();
    }

    private void OnEnable()
    {
        _menu.MediumLevelPassed += OnMediumLevelPassed;
        _winScore.ScoreAdded += OnScoreAdded;
    }

    private void OnDisable()
    {
        _menu.MediumLevelPassed -= OnMediumLevelPassed;
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
