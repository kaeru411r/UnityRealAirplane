using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing : MonoBehaviour
{
    [SerializeField, Range(-1, 1)]
    float _defaultLift;
    [SerializeField, Range(-1, 1)]
    float _upperLimit;
    [SerializeField, Range(-1, 1)]
    float _downerLimit;
    [SerializeField, Range(0, 1)]
    float _efficiency;
    [SerializeField, Range(0, 1)]
    float _drag;

    Rigidbody _rb;

    public float DefaultLift { get => _defaultLift; set => _defaultLift = value; }
    public float UpperLimit { get => _upperLimit; set => _upperLimit = value; }
    public float DownerLimit { get => _downerLimit; set => _downerLimit = value; }

    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (_rb)
        {
            Vector3 velocity = _rb.velocity + _rb.GetRelativePointVelocity(transform.localPosition);

            float power = Mathf.Abs(Vector3.Dot(transform.forward, velocity));
            Vector3 lift = (transform.up * _efficiency - transform.forward) * power * _defaultLift;
            Debug.DrawRay(transform.position, lift);

            float drag = -Vector3.Dot(transform.up, velocity) * _drag;
            Vector3 dragForce = transform.up * drag;
            _rb.AddForceAtPosition(lift + dragForce, transform.position, ForceMode.Force);
        }
    }
}
