using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private DifficultyButton[] _difficultyButtons;
    
    private GameSettings _gameSettings;
    public GameSettings GameSettings => _gameSettings;

    private void OnEnable()
    {
        for (int i = 0; i < _difficultyButtons.Length; i++)
        {
            _difficultyButtons[i].ButtonClicked += OnButtonClicked;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _difficultyButtons.Length; i++)
        {
            _difficultyButtons[i].ButtonClicked -= OnButtonClicked;
        }
    }

    private void OnButtonClicked(GameSettings gameSettings)
    {
        _gameSettings = gameSettings;
    }
}
