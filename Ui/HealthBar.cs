using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _enemyName;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _healthText;

    [SerializeField] private RectTransform _rectTransform;
    bool _coroutineIsStarted = false;


    public bool CanSwitchOff = true;
    public bool EnemyOnUnderAim = false;

    private void Start()
    {
        StartCoroutine(SetHealthBarOff());
    }
    public void ChangeSliderValues(float maxValue, float minValue, float currentValue, float Damage)
    {
        _slider.maxValue = maxValue;
        _slider.minValue = minValue;
        _slider.value = currentValue;
        _healthText.text = $"{currentValue}/{maxValue}";

        if (!_coroutineIsStarted)
            StartCoroutine(ShowDamage(Damage));
    }
    private IEnumerator ShowDamage(float Damage)
    {
        _coroutineIsStarted = true;
        float time = 0;
        _damageText.alpha = 1.0f;
        _damageText.text = "-" + Damage.ToString();
        while (_damageText.alpha > 0)
        {
            time += Time.deltaTime;
            _damageText.alpha = Mathf.Lerp(1, 0, time);
            yield return null;
        }
        _coroutineIsStarted = false;
    }

    public void SetNewHealthBarValue(string zombieName, float maxHealth, float currentHelath)
    {
        _enemyName.text = zombieName;
        _slider.maxValue = maxHealth;
        _slider.value = currentHelath;
        _healthText.text = $"{currentHelath}/{maxHealth}";
    }
    public IEnumerator SetOnHelthBar()
    {
        EnemyOnUnderAim = true;
        float time = 0;
        while (_rectTransform.anchoredPosition.y > -50)
        {
            time += Time.deltaTime;
            _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, Mathf.Lerp(100, -50, time));
            yield return null;
        }
    }
    public bool IsHealthBarInvisible()
    {
        if (_rectTransform.anchoredPosition.y >= 100)
            return true;
        else
            return false;
    }
    public IEnumerator SetHealthBarOff()
    {
        while (true)
        {
            EnemyOnUnderAim = false;
            yield return new WaitForSeconds(3);
            if (!EnemyOnUnderAim)
            {
                float time = 0;
                while (_rectTransform.anchoredPosition.y < 100)
                {
                    time += Time.deltaTime;
                    _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, Mathf.Lerp(-50, 100, time));
                    yield return null;
                }
            }
        }


    }

}
