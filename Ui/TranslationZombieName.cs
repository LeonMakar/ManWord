using UnityEngine;
using YG;

public class TranslationZombieName : MonoBehaviour
{
    [SerializeField] private string _ruName;
    [SerializeField] private string _enName;
    [SerializeField] private string _trName;


    [HideInInspector] public string ZombieName;

    private void OnEnable() => YandexGame.SwitchLangEvent += SwitchLanguage;

    private void Start()
    {
        SwitchLanguage(YandexGame.EnvironmentData.language);
    }

    private void OnDisable() => YandexGame.SwitchLangEvent -= SwitchLanguage;

    private void SwitchLanguage(string lang)
    {
        switch (lang)
        {
            case "ru":
                ZombieName = _ruName;
                break;
            case "tr":
                ZombieName = _trName;
                break;
            default:
                ZombieName = _enName;
                break;
        }
    }

}
