using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    [SerializeField] private Menu _menu;
    [SerializeField] private AnswerButton[] _buttons;
    [SerializeField] private TMP_Text _questionView;

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
    public UnityAction HeartsUpdate;

    private void Awake()
    {
        _questionConfiguration = _menu.GameSettings.Questions;
    }

    private void OnEnable()
    {
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
    }

    private void OnDisable()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].WrongAnswer -= OnWrongAnswer;
            _buttons[i].CorrectAnswer -= OnCorrectAnswer;
        }
    }

    private void OnWrongAnswer()
    {
        _heartsCount--;
        HeartsUpdate?.Invoke();

        if (_heartsCount == 0)
        {
            _menu.gameObject.SetActive(true);
            gameObject.SetActive(false);
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
            LevelComplete?.Invoke();
            _menu.gameObject.SetActive(true);
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
