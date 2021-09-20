using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float Speed = 250.0f;
    public float JumpForce = 12f;

    private Rigidbody2D _rb;
    private Animator _animator;
    private BoxCollider2D _box;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        Vector2 movement = new Vector2(deltaX, _rb.velocity.y);
        _rb.velocity = movement;

        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool isGrounded = hit != null;

        _rb.gravityScale = isGrounded && deltaX == 0 ? 0 : 1;
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }

        MovingPlatform platform = null;
        if (hit != null)
        {
            platform = hit.GetComponent<MovingPlatform>();
        }
        if (platform != null)
        {
            transform.parent = platform.transform;
        }
        else
        {
            transform.parent = null;
        }

        Vector3 pScale = Vector3.one;
        if (platform != null)
        {
            pScale = platform.transform.localScale;
        }

        _animator.SetFloat("speed", Mathf.Abs(deltaX));
        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX) / pScale.x, 1 / pScale.y, 1 / pScale.z);
        }
    }
}
