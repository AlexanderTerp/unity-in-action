using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public float rotationSpeed = 1.5f;

    [SerializeField] private Transform _target;
    private float _rotY;
    private Vector3 _offset;

    void Start()
    {
        _rotY = transform.eulerAngles.y;
        _offset = _target.position - transform.position;
    }

    void LateUpdate()
    {
        float horInput = Input.GetAxis("Horizontal");
        if (horInput != 0)
        {
            _rotY += horInput * rotationSpeed;
        }
        else
        {
            _rotY += Input.GetAxis("Mouse X") * rotationSpeed * 3;
        }

        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
        transform.position = _target.position - (rotation * _offset);
        transform.LookAt(_target);
    }
}
