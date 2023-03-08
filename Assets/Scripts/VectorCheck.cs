using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VectorCheck : MonoBehaviour
{
    [SerializeField]
    Vector3 _force;
    [SerializeField]
    Vector3 _velocity;

    Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb= GetComponent<Rigidbody>();
        _rb.velocity = _velocity;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, _rb.velocity);
        Debug.DrawRay(_rb.centerOfMass + transform.position, _rb.velocity);
    }

    private void FixedUpdate()
    {
        if (_rb)
        {
            _rb.AddForce(_force, ForceMode.Force);
        }
    }
}
