using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Oxygen : MonoBehaviour
{
    [SerializeField] private OxygenData _oxygenData;
    [SerializeField, Range(0.0001f, 1f)] private float _breathDecreaseMultiplier;

    public UnityAction NoOxygen;

    private bool _isBreathes = true;
    private IEnumerator _breathing;

    private Slider _breathSlider;

    private void Awake()
    {
        _breathSlider = GetComponent<Slider>();
        _breathSlider.maxValue = _oxygenData.MaxOxygenAmount;

        _breathing = Breathing();
        StartCoroutine(_breathing);
    }

    private IEnumerator Breathing()
    {
        while (_isBreathes)
        {
            _oxygenData.CurrentOxygenAmount -= Time.timeScale * _breathDecreaseMultiplier;
            _breathSlider.value = _oxygenData.CurrentOxygenAmount;
            if (_oxygenData.CurrentOxygenAmount <= 0)
            {
                _isBreathes = false;
                NoOxygen?.Invoke();
                StopCoroutine(_breathing);
            }
            yield return null;
        }
    }
}
