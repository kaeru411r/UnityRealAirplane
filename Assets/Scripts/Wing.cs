using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing : MonoBehaviour
{
    [SerializeField, Range(-180, 180)]
    float _defaultLift;
    [SerializeField]
    AnimationCurve _liftCoefficientCurve;
    [SerializeField, Range(-180, 180)]
    float _upperLimit;
    [SerializeField, Range(-180, 180)]
    float _downerLimit;
    [SerializeField, Range(0, 1)]
    float _efficiency;
    [SerializeField, Range(0, 1)]
    float _drag;
    [SerializeField]
    float _airDensity = 1.292f;
    [SerializeField]
    float _erea = 0f;

    Rigidbody _rb;
    float Angle { get => _defaultLift; }

    public float DefaultLift
    {
        get => _defaultLift; set
        {
            _defaultLift = Mathf.Clamp(value, -180, 180);
        }
    }
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
            float forward = Vector3.Dot(transform.forward, velocity);
            float up = Vector3.Dot(transform.up, velocity);

            float angle = Vector2.SignedAngle(Vector2.right, new Vector2(forward, up)) + Angle;

            float cl = _liftCoefficientCurve.Evaluate(Mathf.Abs(angle)) * Mathf.Sign(angle);

            float power = 0.5f * _airDensity * velocity.sqrMagnitude * _erea * cl;
            Vector3 lift = Vector3.RotateTowards(velocity.normalized, transform.up, 90f, 0f).normalized *_efficiency * power;
            Vector3 lose = velocity.normalized * power;

            float drag = -Vector3.Dot(transform.up, velocity) * _drag;
            Vector3 dragForce = transform.up * drag;
            _rb.AddForceAtPosition(lift + lose + dragForce, transform.position, ForceMode.Force);
            Debug.Log(power);
        }
    }


    private void Update()
    {
        Vector3 velocity = _rb.velocity + _rb.GetRelativePointVelocity(transform.localPosition);
        float forward = Vector3.Dot(transform.forward, velocity);
        float up = Vector3.Dot(transform.up, velocity);

        float angle = Vector2.SignedAngle(Vector2.right, new Vector2(forward, up)) + Angle;

        float cl = _liftCoefficientCurve.Evaluate(Mathf.Abs(angle)) * Mathf.Sign(angle);

        float power = 0.5f * _airDensity * velocity.sqrMagnitude * _erea * cl;
        Vector3 lift = Vector3.RotateTowards(velocity.normalized, transform.up, 90f, 0f).normalized * _efficiency * power;
        Debug.DrawRay(transform.position, lift, Color.red);
        Debug.DrawRay(transform.position, Vector3.RotateTowards(velocity.normalized, transform.up, 90f, 0f).normalized, Color.blue);
    }
}


//参考サイト https://sites.google.com/view/ronsu900/createfs/wing1