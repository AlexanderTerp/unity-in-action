using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    public float moveSpeed = 6.0f;

    [SerializeField] private Transform _target;

    private CharacterController _charController;

    void Awake()
    {
        _charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Vector3 movement = Vector3.zero;
        // float horInput = Input.GetAxis("Horizontal");
        // float vertInput = Input.GetAxis("Vertical");
        // if (horInput != 0 || vertInput != 0)
        // {
        //     movement.x = horInput;
        //     movement.z = vertInput;
        //     Quaternion tmp = _target.rotation;
        //     _target.eulerAngles = new Vector3(0, _target.eulerAngles.y, 0);
        //     movement = _target.TransformDirection(movement);
        //     _target.rotation = tmp;
        //     transform.rotation = Quaternion.LookRotation(movement);
        // }

        Vector3 movement = Vector3.zero;
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        if (horInput == 0 && vertInput == 0) return;

        if (horInput != 0) movement += _target.right * horInput * moveSpeed;
        if (vertInput != 0) movement += new Vector3(_target.forward.x, 0, _target.forward.z).normalized * vertInput * moveSpeed;
        movement = Vector3.ClampMagnitude(movement, moveSpeed);
        movement *= Time.deltaTime;

        transform.rotation = Quaternion.LookRotation(movement);
        _charController.Move(movement);
    }
}
