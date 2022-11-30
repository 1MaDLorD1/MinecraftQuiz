using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Image))]
public class SoundsOnOff : MonoBehaviour
{
    [SerializeField] private Sprite _soundsOn;
    [SerializeField] private Sprite _soundsOff;
    [SerializeField] private AudioSource _audioSource;

    private Button _button;
    private Image _currentImage;
    private bool _isOn;

    private void Start()
    {
        _isOn = true;

        if(_currentImage == null) _currentImage = GetComponent<Image>();

        if (_button == null) _button = GetComponent<Button>();

        _button.onClick.AddListener(HandleClickButton);
    }

    private void HandleClickButton()
    {
        if(_isOn)
        {
            _audioSource.enabled = false;
            _isOn = false;
            _currentImage.sprite = _soundsOff;
        }
        else
        {
            _audioSource.enabled = true;
            _isOn = true;
            _currentImage.sprite = _soundsOn;
        }
    }
}
