using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MediumButton : DifficultyButton
{
    [SerializeField] private GameSettings _gameSettings;

    protected override void HandleClickButton()
    {
        base.HandleClickButton();
        ButtonClicked?.Invoke(_gameSettings);
    }
}