using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EasyButton : DifficultyButton
{
    [SerializeField] private GameSettings _gameSettings;

    protected override void HandleClickButton()
    {
        ButtonClicked?.Invoke(_gameSettings);
        base.HandleClickButton();
    }
}
