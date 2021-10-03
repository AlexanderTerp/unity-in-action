using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events
{
    public static event Action WeatherUpdated;

    public static void NotifyWeatherUpdate()
    {
        WeatherUpdated?.Invoke();
    }
}
