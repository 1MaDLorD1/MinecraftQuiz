using UnityEngine;
using UnityEngine.UI;
using YG;

public class SaverTest : MonoBehaviour
{
    [SerializeField] InputField integerText;
    [SerializeField] InputField stringifyText;
    [SerializeField] Text systemSavesText;
    [SerializeField] Toggle[] booleanArrayToggle;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Start()
    {
        if (YandexGame.SDKEnabled)
            GetLoad();
    }

    public void Save()
    {
        YandexGame.savesData.Score = int.Parse(integerText.text);

        YandexGame.SaveProgress();
    }

    public void Load() => YandexGame.LoadProgress();

    public void GetLoad()
    {
        integerText.text = string.Empty;
        stringifyText.text = string.Empty;

        integerText.placeholder.GetComponent<Text>().text = YandexGame.savesData.Score.ToString();

        systemSavesText.text = $"Language - {YandexGame.savesData.language}\n" +
        $"First Session - {YandexGame.savesData.isFirstSession}\n" +
        $"Prompt Done - {YandexGame.savesData.promptDone}\n";
    }
}
