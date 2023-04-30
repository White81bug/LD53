using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Oxygen : MonoBehaviour
{
    public static Oxygen Instance;
    [SerializeField] private OxygenData _oxygenData;
    [SerializeField, Range(0.0001f, 1f)] private float _breathDecreaseMultiplier;

    public UnityAction NoOxygen;

    private bool _isBreathes = true;
    private IEnumerator _breathing;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
        
        UIManager.Instance.SetSliderMax(_oxygenData.MaxOxygenAmount);
    }

    public void StartBreathing()
    {
        _breathing = Breathing();
        StartCoroutine(_breathing);
    }

    private IEnumerator Breathing()
    {
        while (_isBreathes)
        {
            _oxygenData.CurrentOxygenAmount -= Time.timeScale * _breathDecreaseMultiplier;
            UIManager.Instance.SetSliderValue(_oxygenData.CurrentOxygenAmount);
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
