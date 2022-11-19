using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ContinueWithAdButton : MonoBehaviour
{
    [SerializeField] protected Game _game;
    [SerializeField] protected ExtraMenu _extraMenu;
    [SerializeField] private Timer _timer;
    [SerializeField] private AnswerButton _answerButton;

    private Button _button;

    public UnityAction HeartAdd;

    private void Start()
    {
        if (_button == null) _button = GetComponent<Button>();

        _button.onClick.AddListener(HandleClickButton);
    }

    protected virtual void HandleClickButton()
    {
        _timer.gameObject.SetActive(true);

        if (_game.HeartsCount <= 0)
        {
            HeartAdd?.Invoke();
        }
        _answerButton.CorrectAnswer?.Invoke();

        _extraMenu.gameObject.SetActive(false);
    }
}
