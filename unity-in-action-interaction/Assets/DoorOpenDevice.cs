using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField] private Vector3 dPos;

    private bool _open;

    public void Operate()
    {
        if (_open)
        {
            transform.position -= dPos;
        }
        else
        {
            transform.position += dPos;
        }
        _open = !_open;
    }

    public void Activate()
    {
        if (!_open)
        {
            transform.position += dPos;
            _open = true;
        }
    }

    public void Deactivate()
    {
        if (_open)
        {
            transform.position -= dPos;
            _open = false;
        }
    }
}
