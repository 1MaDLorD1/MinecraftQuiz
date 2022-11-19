using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WinScore : MonoBehaviour
{
    [SerializeField] private GeneralScore _generalScore;
    [SerializeField] private TMP_Text _scoreView;
    [SerializeField] private Game _game;
    [SerializeField] private EasyButton _easyButton;
    [SerializeField] private MediumButton _mediumButton;
    [SerializeField] private HardButton _hardButton;

    private int _score;
    public int Score => _score;

    public UnityAction ScoreAdded;

    private void OnEnable()
    {
        if (_easyButton.IsStarted)
        {
            _score = _game.HeartsCount;
            Debug.Log("Easy");
        }
        else if (_mediumButton.IsStarted)
        {
            _score = _game.HeartsCount * 2;
            Debug.Log("Medium");
        }
        else if (_hardButton.IsStarted)
        {
            _score = _game.HeartsCount * 3;
            Debug.Log("Hard");
        }

        ScoreAdded?.Invoke();

        _generalScore.Score += _score;

        _scoreView.text = $"{_score}";
    }
}
