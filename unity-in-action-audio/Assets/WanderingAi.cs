using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAi : MonoBehaviour
{
    public float Speed = 3.0f;
    public float ObstacleRange = 5.0f;

    [SerializeField] private GameObject fireballPrefab;

    private GameObject _fireball;
    private bool _isAlive;

    void Start()
    {
        _isAlive = true;
    }

    void Update()
    {
        if (_isAlive) transform.Translate(0, 0, Speed * Time.deltaTime);

        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.GetComponent<PlayerCharacter>())
            {
                if (_fireball == null)
                {
                    _fireball = Instantiate(fireballPrefab) as GameObject;
                    _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                    _fireball.transform.rotation = transform.rotation;
                }
            }
            else if (hit.distance < ObstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }

    public void SetAlive(bool isAlive)
    {
        _isAlive = isAlive;
    }
}
