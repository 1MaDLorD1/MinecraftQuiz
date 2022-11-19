using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GeneralScore : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreView;
    [SerializeField] private WinScore _winScore;

    public int Score { get; set; }

    private void OnEnable()
    {
        _scoreView.text = $"{Score}";
    }
}
