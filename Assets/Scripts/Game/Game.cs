using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using YG;

public class Game : MonoBehaviour
{
    [SerializeField] private Menu _menu;
    [SerializeField] private LoseMenu _loseMenu;
    [SerializeField] private WinMenu _winMenu;
    [SerializeField] private AnswerButton[] _buttons;
    [SerializeField] private TMP_Text _questionView;
    [SerializeField] private Timer _timer;
    [SerializeField] private ContinueWithAdButton _continueWithAdButton;

    private string _question;
    private string _correctAnswer;
    private string[] _wrongAnswers;
    private List<QuestionConfiguration> _listOfQuestions;
    private QuestionConfiguration currentQuestion;

    private int _heartsCount = 4;
    public int HeartsCount => _heartsCount;

    private QuestionConfiguration[] _questionConfiguration;
    public QuestionConfiguration[] Questions => _questionConfiguration;

    public UnityAction<int> QuestionAnswered;
    public UnityAction LevelComplete;
    public UnityAction GameComplete;
    public UnityAction HeartsUpdate;
    public UnityAction HeartAdd;
    public UnityAction PlayerDead;
    public UnityAction<int> QuestionConfigurationSetted;

    private void OnEnable()
    {
        _questionConfiguration = _menu.GameSettings.Questions;

        QuestionConfigurationSetted?.Invoke(_questionConfiguration.Length);

        _heartsCount = 4;

        _listOfQuestions = _questionConfiguration.ToList();
        currentQuestion = _listOfQuestions[Random.Range(0, _listOfQuestions.Count)];
        _correctAnswer = currentQuestion.CorrectAnswer;
        _wrongAnswers = currentQuestion.WrongAnswers;
        _question = currentQuestion.Question;
        _questionView.text = _question;

        CreateRandomAnswers();

        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].WrongAnswer += OnWrongAnswer;
            _buttons[i].CorrectAnswer += OnCorrectAnswer;
        }

        _timer.TimeEnd += OnTimeEnd;
        _continueWithAdButton.HeartAdd += OnHeartAdd;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].WrongAnswer -= OnWrongAnswer;
            _buttons[i].CorrectAnswer -= OnCorrectAnswer;
        }

        _timer.TimeEnd -= OnTimeEnd;
        _continueWithAdButton.HeartAdd -= OnHeartAdd;
    }

    private void OnHeartAdd()
    {
        _heartsCount++;
        HeartAdd?.Invoke();
    }

    private void OnTimeEnd()
    {
        _timer.gameObject.SetActive(false);

        _heartsCount--;
        HeartsUpdate?.Invoke();

        if (_heartsCount == 0)
        {
            PlayerDead?.Invoke();
            _loseMenu.gameObject.SetActive(true);
        }
        else
        {
            _timer.gameObject.SetActive(true);
        }
    }

    private void OnWrongAnswer()
    {
        _heartsCount--;
        HeartsUpdate?.Invoke();

        if (_heartsCount == 0)
        {
            PlayerDead?.Invoke();
            _loseMenu.gameObject.SetActive(true);
            _timer.gameObject.SetActive(false);
        }
    }

    private void OnCorrectAnswer()
    {
        _listOfQuestions.Remove(currentQuestion);

        if (_listOfQuestions.Count > 0)
        {
            QuestionAnswered?.Invoke(_listOfQuestions.Count);
            currentQuestion = _listOfQuestions[Random.Range(0, _listOfQuestions.Count)];
            _correctAnswer = currentQuestion.CorrectAnswer;
            _wrongAnswers = currentQuestion.WrongAnswers;
            _question = currentQuestion.Question;
            _questionView.text = _question;
            CreateRandomAnswers();
        }
        else
        {
            YandexGame.FullscreenShow();
            GameComplete?.Invoke();
            _winMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void CreateRandomAnswers()
    {
        List<string> answers = _wrongAnswers.Append(_correctAnswer).ToList();

        for (int i = 0; i < _buttons.Length; i++)
        {
            var currentAnswer = answers[Random.Range(0, answers.Count)];
            _buttons[i].GetComponentInChildren<TMP_Text>().text = currentAnswer;
            if (currentAnswer == _correctAnswer)
                _buttons[i].IsCorrect = true;
            else
                _buttons[i].IsCorrect = false;
            answers.Remove(currentAnswer);
        }
    }
}
