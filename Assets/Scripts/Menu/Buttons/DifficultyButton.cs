using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class DifficultyButton : MonoBehaviour
{
    [SerializeField] private string _difficulty;
    [SerializeField] protected Game _game;
    [SerializeField] protected Menu _menu;
    [SerializeField] public bool IsUnlocked;
    [SerializeField] public TMP_Text LevelDifficulty;

    protected Button _button;

    public UnityAction<GameSettings> ButtonClicked;

    private void Start()
    {
        if (_button == null) _button = GetComponent<Button>();

        LevelDifficulty.text = _difficulty.ToString();
        if(IsUnlocked)
            _button.onClick.AddListener(HandleClickButton);
    }

    protected virtual void HandleClickButton()
    {
        if (IsUnlocked)
        {
            _game.gameObject.SetActive(true);
            _menu.gameObject.SetActive(false);
        }
    }
}
