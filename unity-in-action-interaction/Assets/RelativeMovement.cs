using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    public float moveSpeed = 6.0f;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.81f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;

    [SerializeField] private Transform _target = null;

    private CharacterController _charController;
    private Animator _animator;
    private float _vertSpeed;
    private ControllerColliderHit _contact;

    void Awake()
    {
        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _vertSpeed = minFall;
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

        if (horInput != 0 || vertInput != 0)
        {
            movement += _target.right * horInput * moveSpeed;
            movement += new Vector3(_target.forward.x, 0, _target.forward.z).normalized * vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);
            transform.rotation = Quaternion.LookRotation(movement);
        }

        _animator.SetFloat("Speed", movement.sqrMagnitude);

        bool hitGround = false;
        RaycastHit hit;
        if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (_charController.height + _charController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }

        if (hitGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _vertSpeed = jumpSpeed;
            }
            else
            {
                _vertSpeed = minFall;
                _animator.SetBool("Jumping", false);
            }
        }
        else
        {
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)
            {
                _vertSpeed = terminalVelocity;
            }
            if (_contact != null)
            {
                _animator.SetBool("Jumping", true);
            }
            if (_charController.isGrounded)
            {
                if (Vector3.Dot(movement, _contact.normal) < 0)
                {
                    movement = _contact.normal * moveSpeed;
                }
                else
                {
                    movement += _contact.normal * moveSpeed;
                }
            }
        }
        movement.y = _vertSpeed;

        movement *= Time.deltaTime;
        _charController.Move(movement);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;
    }
}