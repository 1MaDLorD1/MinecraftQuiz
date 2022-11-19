using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AgainButton : MonoBehaviour
{
    [SerializeField] protected Game _game;
    [SerializeField] protected ExtraMenu _extraMenu;
    [SerializeField] private Timer _timer;

    private Button _button;

    private void Start()
    {
        if (_button == null) _button = GetComponent<Button>();

         _button.onClick.AddListener(HandleClickButton);
    }

    protected virtual void HandleClickButton()
    {
        if (_game.gameObject.activeSelf == true)
        {
            _timer.gameObject.SetActive(true);
            _game.gameObject.SetActive(false);
        }

        _game.gameObject.SetActive(true);

        if (_extraMenu.GetComponent<WinMenu>() != null)
        {
            _game.LevelComplete?.Invoke();
        }

        _extraMenu.gameObject.SetActive(false);
    }
}
