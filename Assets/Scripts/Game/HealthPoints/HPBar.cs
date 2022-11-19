using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Heart _heartPrefab;
    [SerializeField] private EmptyHeart _emptyHeartPrefab;
    [SerializeField] private Transform _heartsPosition;
    [SerializeField] private Game _game;

    private List<Heart> _hearts;
    private int _heartsCount;

    private void Awake()
    {
        _hearts = new List<Heart>();

        _heartsCount = _game.HeartsCount;
    }

    private void OnEnable()
    {
        for (int i = 0; i < _heartsCount; i++)
        {
            _hearts.Add(Instantiate(_heartPrefab, _heartsPosition));
        }

        _game.HeartsUpdate += OnHeartsUpdate;
        _game.HeartAdd += OnHeartAdd;
    }

    private void OnDisable()
    {
        DestroyAllHearts();

        _game.HeartsUpdate -= OnHeartsUpdate;
        _game.HeartAdd -= OnHeartAdd;
    }

    private void OnHeartAdd()
    {
        DestroyAllHearts();

        Instantiate(_heartPrefab, _heartsPosition);

        for (int i = 0; i < 3; i++)
        {
            Instantiate(_emptyHeartPrefab, _heartsPosition);
        }
    }

    private void OnHeartsUpdate()
    {
        if (GetComponentInChildren<Heart>() != null)
        {
            Destroy(GetComponentInChildren<Heart>().gameObject);
            Instantiate(_emptyHeartPrefab, _heartsPosition);
        }
    }

    private void DestroyAllHearts()
    {
        var hearts = GetComponentsInChildren<Heart>();
        var emptyHearts = GetComponentsInChildren<EmptyHeart>();

        for (int i = 0; i < hearts.Length; i++)
        {
            if (GetComponentInChildren<Heart>() != null)
            {
                Destroy(hearts[i].gameObject);
            }
        }

        for (int i = 0; i < emptyHearts.Length; i++)
        {
            if (GetComponentInChildren<EmptyHeart>() != null)
            {
                Destroy(emptyHearts[i].gameObject);
            }
        }
    }
}
