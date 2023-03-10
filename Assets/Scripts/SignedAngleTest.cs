using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignedAngleTest : MonoBehaviour
{
    [SerializeField] Transform _to;
    [SerializeField] float _angle;

    private void Start()
    {
    }
    private void OnDrawGizmos()
    {
        if (_to)
        {
            float forward = Vector3.Dot(transform.forward, _to.forward);
            float up = Vector3.Dot(transform.up, _to.forward);

            float angle = Vector2.SignedAngle(Vector2.right, new Vector2(forward, up));

            //Gizmos.DrawRay(transform.position, transform.forward);
            //Gizmos.DrawRay(_to.position, _to.forward);
            Debug.Log(angle);

            Debug.DrawRay(transform.position, Vector3.RotateTowards(transform.forward, _to.forward, _angle, 10f).normalized, Color.red);
        }
    }
}
