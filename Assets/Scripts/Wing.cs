using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing : MonoBehaviour
{
    [SerializeField]
    float _lift;
    [SerializeField, Range(0, 1)]
    float _drag;

    Rigidbody _rb;

    public float Lift { get => _lift; set => _lift = value; }


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
            Vector3 lift = (transform.up - Mathf.Sign(Vector3.Dot(transform.forward, velocity)) * transform.forward) * power * _lift;
            Debug.DrawRay(transform.position, lift);

            float drag = -Vector3.Dot(transform.up, velocity) * _drag;
            Vector3 dragForce = transform.up * drag;
            _rb.AddForceAtPosition(lift + dragForce, transform.position, ForceMode.Force);
        }
    }
}
