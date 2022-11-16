using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Setting", menuName = "Setting")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private QuestionConfiguration[] _questions;

    public QuestionConfiguration[] Questions => _questions;
}
