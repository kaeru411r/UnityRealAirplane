using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing : MonoBehaviour
{
    [SerializeField] float _lift;

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
            float power = Mathf.Abs(Vector3.Dot(transform.forward, _rb.velocity));
            Vector3 force = (transform.up - Mathf.Sign(Vector3.Dot(transform.forward, _rb.velocity)) * transform.forward) * power * _lift;
            Debug.DrawRay(transform.position, force);
            _rb.AddForceAtPosition(force, transform.position, ForceMode.Force);
        }
    }
}
