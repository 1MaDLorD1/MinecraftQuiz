using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YG;

public class Saver : MonoBehaviour
{
	[SerializeField] private GeneralScore _generalScore;
	[SerializeField] private Menu _menu;
	[SerializeField] private DifficultyButton[] _difficultyButtons;

	private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

	private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

	public UnityAction DataLoaded;

	private void Start()
	{
		if (YandexGame.SDKEnabled == true)
		{
			GetLoad();
		}
	}

	public void GetLoad()
	{
		_generalScore.Score = YandexGame.savesData.Score;

        if (YandexGame.savesData._openDifficultyButtons[0])
		{
			Destroy(_difficultyButtons[0].GetComponentInChildren<Padlock>().gameObject);
			_difficultyButtons[0].IsUnlocked = true;
			_difficultyButtons[0].LevelDifficulty.color = Color.white;
			_menu.MediumLevelPassed?.Invoke();
		}
		
		if(YandexGame.savesData._openDifficultyButtons[1])
		{
			Destroy(_difficultyButtons[1].GetComponentInChildren<Padlock>().gameObject);
			_difficultyButtons[1].IsUnlocked = true;
			_difficultyButtons[1].LevelDifficulty.color = Color.white;
			_menu.HardLevelPassed?.Invoke();
		}

		DataLoaded?.Invoke();
	}


	public void MySave()
	{
		YandexGame.savesData.Score = _generalScore.Score;

        for (int i = 0; i < _difficultyButtons.Length; i++)
        {
			if(_difficultyButtons[i].IsUnlocked)
            {
				YandexGame.savesData._openDifficultyButtons[i] = true;
			}
        }

		YandexGame.SaveProgress();
	}
}
