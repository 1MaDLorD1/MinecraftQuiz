using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Button))]
public class ContinueWithAdButton : MonoBehaviour
{
    [SerializeField] protected Game _game;
    [SerializeField] protected ExtraMenu _extraMenu;
    [SerializeField] private Timer _timer;
    [SerializeField] private AnswerButton _answerButton;

    private Button _button;

    public UnityAction ButtonClicked;
    public UnityAction HeartAdd;

    private void Start()
    {
        if (_button == null) _button = GetComponent<Button>();

        _button.onClick.AddListener(HandleClickButton);
    }

    private void OnEnable()
    {
        YandexGame.OpenVideoEvent += OnOpenVideoEvent;
        YandexGame.CloseVideoEvent += OnCloseVideoEvent;
        YandexGame.ErrorVideoEvent += OnErrorVideoEvent;
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
        YandexGame.OpenVideoEvent -= OnOpenVideoEvent;
        YandexGame.CloseVideoEvent -= OnCloseVideoEvent;
        YandexGame.ErrorVideoEvent -= OnErrorVideoEvent;
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    void OnOpenVideoEvent()
    {
        Time.timeScale = 0f;
    }

    void OnCloseVideoEvent()
    {
    }

    void OnErrorVideoEvent()
    {
        Debug.Log("Ошибка просмотра рекламы!");
    }

    void Rewarded(int id)
    {
        if (_game.HeartsCount <= 0)
        {
            HeartAdd?.Invoke();
        }

        _answerButton.CorrectAnswer?.Invoke();

        Time.timeScale = 1f;

        _timer.gameObject.SetActive(true);

        _extraMenu.gameObject.SetActive(false);
    }

    protected virtual void HandleClickButton()
    {
        YandexGame.RewVideoShow(0);

        ButtonClicked?.Invoke();
    }
}
