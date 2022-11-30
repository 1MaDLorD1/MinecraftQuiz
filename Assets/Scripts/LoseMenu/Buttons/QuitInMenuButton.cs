using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class QuitInMenuButton : MonoBehaviour
{
    [SerializeField] protected Menu _menu;
    [SerializeField] protected ExtraMenu _extraMenu;
    [SerializeField] private Game _game;
    [SerializeField] private Timer _timer;

    private Button _button;

    public UnityAction ButtonClicked;

    private void Start()
    {
        if (_button == null) _button = GetComponent<Button>();

        _button.onClick.AddListener(HandleClickButton);
    }

    protected virtual void HandleClickButton()
    {
        ButtonClicked?.Invoke();

        if (_game.gameObject.activeSelf == true)
        {
            _timer.gameObject.SetActive(true);
            _game.gameObject.SetActive(false);
        }

        _menu.gameObject.SetActive(true);

        if(_extraMenu.GetComponent<WinMenu>() != null)
        {
            _game.LevelComplete?.Invoke();
        }

        _extraMenu.gameObject.SetActive(false);
    }
}
