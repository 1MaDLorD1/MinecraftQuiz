using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private DifficultyButton[] _difficultyButtons;
    [SerializeField] private Game _game;
    
    private GameSettings _gameSettings;
    public GameSettings GameSettings => _gameSettings;

    private bool _mediumLevelStarted;

    public UnityAction MediumLevelPassed;
    public UnityAction HardLevelPassed;

    private void OnEnable()
    {
        for (int i = 0; i < _difficultyButtons.Length; i++)
        {
            _difficultyButtons[i].ButtonClicked += OnButtonClicked;
        }

        _game.LevelComplete += OnLevelComplete;

        GetComponentInChildren<MediumButton>().MediumLevelStarted += OnMediumLevelStarted;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _difficultyButtons.Length; i++)
        {
            _difficultyButtons[i].ButtonClicked -= OnButtonClicked;
        }

        _game.LevelComplete -= OnLevelComplete;

        GetComponentInChildren<MediumButton>().MediumLevelStarted -= OnMediumLevelStarted;
    }

    private void OnMediumLevelStarted()
    {
        _mediumLevelStarted = true;
    }


    private void OnButtonClicked(GameSettings gameSettings)
    {
        _gameSettings = gameSettings;
    }

    private void OnLevelComplete()
    {
        var mediumButtonPadlock = GetComponentInChildren<MediumButton>().GetComponentInChildren<Padlock>();

        for (int i = 0; i < _difficultyButtons.Length; i++)
        {
            if(_difficultyButtons[i].TryGetComponent(out MediumButton mediumButton))
            {
                mediumButtonPadlock = mediumButton.GetComponentInChildren<Padlock>();
                if (mediumButtonPadlock != null)
                {
                    Destroy(mediumButton.GetComponentInChildren<Padlock>().gameObject);
                    mediumButton.IsUnlocked = true;
                    mediumButton.LevelDifficulty.color = Color.white;
                    MediumLevelPassed?.Invoke();
                }
            }
            else if(_difficultyButtons[i].TryGetComponent(out HardButton hardButton))
            {
                var hardButtonPudlock = hardButton.GetComponentInChildren<Padlock>();
                if (hardButtonPudlock != null && mediumButtonPadlock == null && _mediumLevelStarted)
                {
                    Destroy(hardButton.GetComponentInChildren<Padlock>().gameObject);
                    hardButton.IsUnlocked = true;
                    hardButton.LevelDifficulty.color = Color.white;
                    HardLevelPassed?.Invoke();
                }
            }
        }

        _mediumLevelStarted = false;
    }
}
