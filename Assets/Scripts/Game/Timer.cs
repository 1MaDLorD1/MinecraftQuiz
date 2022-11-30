using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _coolDown;
    [SerializeField] private TMP_Text _timerView;
    [SerializeField] private Game _game;

    private float _timer;

    public UnityAction TimeEnd;

    private void Awake()
    {
        _timerView.text = $"{_coolDown}:0";
    }

    private void OnEnable()
    {
        _timer = _coolDown;
        _game.QuestionAnswered += OnQuestionAnswered;
        _game.LevelComplete += OnLevelComplete;
    }

    private void OnDisable()
    {
        _game.QuestionAnswered -= OnQuestionAnswered;
        _game.LevelComplete -= OnLevelComplete;
    }

    void Update()
    {
        if(_timer > 0)
        {
            _timer -= Time.deltaTime;
            var fractionalPart = (_timer - (int)_timer) / 0.1;
            _timerView.text = $"{(int)_timer}:{(int)fractionalPart}";
        }
        else
        {
            _timer = _coolDown;
            TimeEnd?.Invoke();
        }
    }

    private void OnQuestionAnswered(int unnecessaryValue)
    {
        _timer = _coolDown;
    }

    private void OnLevelComplete()
    {
        _timer = _coolDown;
    }
}
