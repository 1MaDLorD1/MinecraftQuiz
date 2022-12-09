using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using YG;

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

    private const int _scoreIncreaser = 2;

    private void OnEnable()
    {
        if (_easyButton.IsStarted)
        {
            _score = _game.HeartsCount * _scoreIncreaser;
        }
        else if (_mediumButton.IsStarted)
        {
            _score = _game.HeartsCount * _scoreIncreaser * 2;
        }
        else if (_hardButton.IsStarted)
        {
            _score = _game.HeartsCount * _scoreIncreaser * 3;
        }

        ScoreAdded?.Invoke();

        _generalScore.Score += _score;

        _scoreView.text = $"{_score}";
    }
}
