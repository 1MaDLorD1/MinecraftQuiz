using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum SoundsIndexes
{
    ÑlickButton,
    WrongAnswer,
    Death,
    CorrectAnswer,
    LevelComplete
}

[RequireComponent(typeof(AudioSource))]
public class ButtonClickAudio : MonoBehaviour
{
    [SerializeField] private DifficultyButton[] _difficultyButtons;
    [SerializeField] private AnswerButton[] _answerButtons;
    [SerializeField] private AgainButton[] _againButtons;
    [SerializeField] private QuitInMenuButton[] _quitInMenuButtons;
    [SerializeField] private ContinueWithAdButton _continueWithAdButton;
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private Game _game;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _difficultyButtons.Length; i++)
        {
            _difficultyButtons[i].ButtonClicked += OnButtonClick;
        }

        for (int i = 0; i < _answerButtons.Length; i++)
        {
            _answerButtons[i].ButtonClicked += OnButtonClick;
            _answerButtons[i].CorrectAnswer += OnCorrectAnswer;
        }

        for (int i = 0; i < _againButtons.Length; i++)
        {
            _againButtons[i].ButtonClicked += OnButtonClick;
        }

        for (int i = 0; i < _quitInMenuButtons.Length; i++)
        {
            _quitInMenuButtons[i].ButtonClicked += OnButtonClick;
        }
        
        _continueWithAdButton.ButtonClicked += OnButtonClick;
        _game.PlayerDead += OnPlayerDead;
        _game.GameComplete += OnGameComplete;
        _game.HeartsUpdate += OnHeartsUpdate;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _difficultyButtons.Length; i++)
        {
            _difficultyButtons[i].ButtonClicked -= OnButtonClick;
        }

        for (int i = 0; i < _answerButtons.Length; i++)
        {
            _answerButtons[i].ButtonClicked -= OnButtonClick;
            _answerButtons[i].CorrectAnswer -= OnCorrectAnswer;
        }

        for (int i = 0; i < _againButtons.Length; i++)
        {
            _againButtons[i].ButtonClicked -= OnButtonClick;
        }

        for (int i = 0; i < _quitInMenuButtons.Length; i++)
        {
            _quitInMenuButtons[i].ButtonClicked -= OnButtonClick;
        }

        _continueWithAdButton.ButtonClicked -= OnButtonClick;
        _game.PlayerDead -= OnPlayerDead;
        _game.GameComplete -= OnGameComplete;
        _game.HeartsUpdate -= OnHeartsUpdate;
    }

    private void OnHeartsUpdate()
    {
        _audioSource.PlayOneShot(_audioClips[(int)SoundsIndexes.WrongAnswer]);
    }

    private void OnGameComplete()
    {
        _audioSource.PlayOneShot(_audioClips[(int)SoundsIndexes.LevelComplete]);
    }

    private void OnCorrectAnswer()
    {
        _audioSource.PlayOneShot(_audioClips[(int)SoundsIndexes.CorrectAnswer]);
    }

    private void OnPlayerDead()
    {
        _audioSource.PlayOneShot(_audioClips[(int)SoundsIndexes.Death]);
    }

    private void OnButtonClick(GameSettings unnecessaryValue)
    {
        _audioSource.PlayOneShot(_audioClips[(int)SoundsIndexes.ÑlickButton]);
    }

    private void OnButtonClick()
    {
        _audioSource.PlayOneShot(_audioClips[(int)SoundsIndexes.ÑlickButton]);
    }
}
