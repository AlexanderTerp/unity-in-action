using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{
    public bool requireKey;

    [SerializeField] private GameObject[] targets;

    void OnTriggerEnter(Collider other)
    {
        if (requireKey && Managers.Inventory.EquippedItem != "key") return;

        foreach (GameObject target in targets)
        {
            target.SendMessage("Activate");
        }
    }
    void OnTriggerExit(Collider other)
    {
        foreach (GameObject target in targets)
        {
            target.SendMessage("Deactivate");
        }
    }
}
