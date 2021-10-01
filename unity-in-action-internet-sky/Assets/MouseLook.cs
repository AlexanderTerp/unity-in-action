using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float SensitivityX = 30f;
    public float SensitivityY = 30f;

    public float MinimumVert = -90.0f;
    public float MaximumVert = 90.0f;

    private float _rotationX = 0;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.freezeRotation = true;
    }

    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * SensitivityX * Time.deltaTime, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * SensitivityY * Time.deltaTime;
            _rotationX = Mathf.Clamp(_rotationX, MinimumVert, MaximumVert);
            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else
        {
            _rotationX -= Input.GetAxis("Mouse Y") * SensitivityY * Time.deltaTime;
            _rotationX = Mathf.Clamp(_rotationX, MinimumVert, MaximumVert);
            float delta = Input.GetAxis("Mouse X") * SensitivityX * Time.deltaTime;
            float rotationY = transform.localEulerAngles.y + delta;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}