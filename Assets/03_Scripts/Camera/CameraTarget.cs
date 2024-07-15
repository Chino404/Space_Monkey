using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed;
    private float distance;
    [SerializeField] Vector3 pos;
    private Vector3 _velocity;

    private void Start()
    {
        distance = Vector3.Distance(_target.position, transform.position);
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(_target.position, transform.position) <= distance) return;
        //transform.forward = _target.forward;
        _velocity.z = _target.position.z - transform.position.z;
        //_velocity.x = 0;
        //_velocity.y = 0;
        _velocity.Normalize();
        if (_target.position.y + pos.y != transform.position.y)
            _velocity.y = _target.position.y+pos.y - transform.position.y;
        else _velocity.y = 0;
        transform.position += _velocity * _smoothSpeed * Time.deltaTime;
    }
}
