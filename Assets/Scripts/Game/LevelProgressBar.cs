using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelProgressBar : MonoBehaviour
{
    [SerializeField] private float _filledDuration;
    [SerializeField] private Slider _slider;
    [SerializeField] private Game _game;
    [SerializeField] private TMP_Text _leftOf;

    private float _questionsStartCount;

    private void Start()
    {
        _questionsStartCount = _game.Questions.Length;
    }

    private void OnEnable()
    {
        _game.QuestionAnswered += OnQuestionAnswered;
        _slider.value = 0;
        _leftOf.text = $"0/{_questionsStartCount}";
    }

    private void OnDisable()
    {
        _game.QuestionAnswered -= OnQuestionAnswered;
    }

    private void OnQuestionAnswered(int size)
    {
        if (_questionsStartCount != 0)
        {
            _slider.DOValue((_questionsStartCount - size) / _questionsStartCount, _filledDuration);
            _leftOf.text = $"{_questionsStartCount - size}/{_questionsStartCount}";
        }
    }
}
