using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class WeatherManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }
    public float CloudValue { get; private set; }

    private NetworkService _network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Weather manager starting...");
        _network = service;
        StartCoroutine(_network.GetWeatherXML(OnXMLDataLoaded));
        Status = ManagerStatus.Initializing;
    }

    public void OnXMLDataLoaded(string data)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(data);
        XmlNode root = doc.DocumentElement;
        XmlNode node = root.SelectSingleNode("clouds");
        string value = node.Attributes["value"].Value;
        CloudValue = Convert.ToInt32(value) / 100f;
        Debug.Log("Value: " + CloudValue);

        Events.NotifyWeatherUpdate();
        Status = ManagerStatus.Started;
    }
}