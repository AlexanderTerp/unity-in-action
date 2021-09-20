using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 FinishPos = Vector3.zero;
    public float SpeedPercentPerSecond = 0.5f;

    private Vector3 _startPos;
    private float _trackPercent = 0;
    private int _direction = 1;

    void Start()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        _trackPercent += _direction * SpeedPercentPerSecond * Time.deltaTime;
        float x = (FinishPos.x - _startPos.x) * _trackPercent + _startPos.x;
        float y = (FinishPos.y - _startPos.y) * _trackPercent + _startPos.y;
        transform.position = new Vector3(x, y, _startPos.z);
        if ((_direction == 1 && _trackPercent >= 1f) ||
            (_direction == -1 && _trackPercent <= 0f))
        {
            _direction *= -1;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 deltaPos = FinishPos - transform.position;
        Vector3 targetTopLeft = new Vector3(transform.position.x - transform.localScale.x / 2 + deltaPos.x, transform.position.y + transform.localScale.y / 2 + deltaPos.y);
        Vector3 targetTopRight = new Vector3(transform.position.x + transform.localScale.x / 2 + deltaPos.x, transform.position.y + transform.localScale.y / 2 + deltaPos.y);
        Vector3 targetBottomRight = new Vector3(transform.position.x + transform.localScale.x / 2 + deltaPos.x, transform.position.y - transform.localScale.y / 2 + deltaPos.y);
        Vector3 targetBottomLeft = new Vector3(transform.position.x - transform.localScale.x / 2 + deltaPos.x, transform.position.y - transform.localScale.y / 2 + deltaPos.y);

        Gizmos.DrawLine(transform.position, FinishPos);
        Gizmos.DrawLine(targetTopLeft, targetTopRight);
        Gizmos.DrawLine(targetTopRight, targetBottomRight);
        Gizmos.DrawLine(targetBottomRight, targetBottomLeft);
        Gizmos.DrawLine(targetBottomLeft, targetTopLeft);
    }
}
