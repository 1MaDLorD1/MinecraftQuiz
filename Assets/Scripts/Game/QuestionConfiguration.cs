using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Question")]
public class QuestionConfiguration : ScriptableObject
{
    [SerializeField] private string _question;
    [SerializeField] private string _correctAnswer;
    [SerializeField] private string[] _wrongAnswers = new string[_answersCount];

    private const int _answersCount = 3;

    public string Question => _question;
    public string[] WrongAnswers => _wrongAnswers;
    public string CorrectAnswer => _correctAnswer;
}
