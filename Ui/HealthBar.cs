using System;
using System.Collections;
using TMPro;
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
    private bool _isOnAim;
    private bool _isGameObjectActive;


    private void OnEnable()
    {
        StartCoroutine(SwitchOffBar());
        _coroutineIsStarted = false;

    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    public void ChangeSliderValues(float maxValue, float minValue, float currentValue, float Damage)
    {
        _slider.maxValue = maxValue;
        _slider.minValue = minValue;
        _slider.value = currentValue;
        _healthText.text = $"{currentValue}/{maxValue}";

        if (!_coroutineIsStarted && _isGameObjectActive)
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
        _isOnAim = false;

    }

    public void SetNewHealthBarValue(string zombieName, float maxHealth, float currentHelath)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            _isGameObjectActive = true;
        }
        _enemyName.text = zombieName;
        _slider.maxValue = maxHealth;
        _slider.value = currentHelath;
        _healthText.text = $"{currentHelath}/{maxHealth}";
        _isOnAim = true;

    }

    private IEnumerator SwitchOffBar()
    {
        while (true)
        {
            _isOnAim = false;
            yield return new WaitForSeconds(2);
            if (!_isOnAim)
            {
                gameObject.SetActive(false);
                _isGameObjectActive = false;

            }
        }
    }
}
