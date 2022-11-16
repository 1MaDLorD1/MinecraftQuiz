using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Padlock : MonoBehaviour
{
    [SerializeField] private Game _game;

    private void OnEnable()
    {
        _game.LevelComplete += OnLevelComplete;
    }

    private void OnDisable()
    {
        _game.LevelComplete -= OnLevelComplete;
    }

    private void OnLevelComplete()
    {
        gameObject.SetActive(false);
    }
}
