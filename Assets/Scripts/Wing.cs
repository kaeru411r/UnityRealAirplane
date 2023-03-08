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
            Vector3 velocity = _rb.velocity + PointVelocity(_rb.angularVelocity, _rb.transform.position + transform.localPosition);

            float power = Mathf.Abs(Vector3.Dot(transform.forward, velocity));
            Vector3 lift = (transform.up - Mathf.Sign(Vector3.Dot(transform.forward, velocity)) * transform.forward) * power * _lift;
            Debug.DrawRay(transform.position, lift);

            float drag = Vector3.Dot(transform.up, velocity) * _drag;
            Vector3 dragForce = transform.up * -Vector3.Dot(transform.up, velocity) * drag;
            _rb.AddForceAtPosition(lift + dragForce, transform.position, ForceMode.Force);


        }
    }
    Vector3 PointVelocity(Vector3 anglerVelocity, Vector3 point)
    {
        float rx = Vector2.Distance(new Vector2(point.y, point.z), Vector2.zero) * anglerVelocity.x;
        float ry = Vector2.Distance(new Vector2(point.x, point.z), Vector2.zero) * -anglerVelocity.y;
        float rz = Vector2.Distance(new Vector2(point.y, point.x), Vector2.zero) * anglerVelocity.z;

        Vector2 yz = new Vector2(-point.z, point.y).normalized * rx * Mathf.PI;
        Vector2 xz = new Vector2(-point.z, point.x).normalized * ry * Mathf.PI;
        Vector2 xy = new Vector2(-point.y, point.x).normalized * rz * Mathf.PI;

        Vector3 result = new(xy.x + xz.x, yz.x + xy.y, yz.y + xz.y);

        return result;
    }
}
