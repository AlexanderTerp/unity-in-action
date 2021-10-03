using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField] private Material sky;
    [SerializeField] private Light sun;

    private float _fullIntensity;

    void Start()
    {
        _fullIntensity = sun.intensity;
    }

    void Awake()
    {
        Events.WeatherUpdated += OnWeatherUpdated;
    }

    void OnDestroy()
    {
        Events.WeatherUpdated -= OnWeatherUpdated;
    }

    private void OnWeatherUpdated()
    {
        SetOvercast(Managers.Weather.CloudValue);
    }

    private void SetOvercast(float value)
    {
        sky.SetFloat("_Blend", value);
        sun.intensity = _fullIntensity - (_fullIntensity * value);
    }
}
