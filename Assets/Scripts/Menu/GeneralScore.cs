using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using YG;

public class GeneralScore : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreView;
    [SerializeField] private WinScore _winScore;
    [SerializeField] private Saver _saver;

    public int Score { get; set; }

    private void OnEnable()
    {
        _scoreView.text = $"{Score}";
        _saver.DataLoaded += OnDataLoaded;
    }

    private void OnDisable()
    {
        _saver.DataLoaded -= OnDataLoaded;
    }

    private void OnDataLoaded()
    {
        _scoreView.text = $"{Score}";
    }
}
