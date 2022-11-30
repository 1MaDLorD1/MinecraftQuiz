using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AnswerButton : MonoBehaviour
{
    private Button _button;

    public bool IsCorrect { get; set; }

    public UnityAction CorrectAnswer;
    public UnityAction WrongAnswer;
    public UnityAction ButtonClicked;

    private void Start()
    {
        if (_button == null) _button = GetComponent<Button>();

        _button.onClick.AddListener(HandleClickButton);
    }

    private void HandleClickButton()
    {
        ButtonClicked?.Invoke();

        if (IsCorrect)
            CorrectAnswer?.Invoke();
        else
            WrongAnswer?.Invoke();
    }
}
