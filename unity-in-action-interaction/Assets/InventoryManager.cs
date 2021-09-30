using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }

    private Dictionary<string, int> _items;

    public void Startup()
    {
        Debug.Log("Inventory manager starting...");
        _items = new Dictionary<string, int>();
        Status = ManagerStatus.Started;
    }

    public void AddItem(string name)
    {
        if (_items.ContainsKey(name))
        {
            _items[name] += 1;
        }
        else
        {
            _items[name] = 1;
        }
        DisplayItems();
    }

    private void DisplayItems()
    {
        string itemDisplay = "Items: ";
        foreach (KeyValuePair<string, int> item in _items)
        {
            itemDisplay += item.Key + "(" + item.Value + ") ";
        }
        Debug.Log(itemDisplay);
    }
}