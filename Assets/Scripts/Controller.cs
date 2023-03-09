using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] 
    float _pitch;
    [SerializeField] 
    float _roll;
    [SerializeField] 
    Wing[] _pitchWings;
    [SerializeField] 
    Wing[] _rightWings;
    [SerializeField] 
    Wing[] _leftWings;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        if(_pitchWings != null && _pitchWings.Length > 0)
        {
            foreach(Wing wing in _pitchWings)
            {
                if (wing)
                {
                    wing.DefaultLift = _pitch / 100;
                }
            }
        }
        //if(_rightWings != null && _rightWings.Length > 0)
        //{
        //    foreach(Wing wing in _rightWings)
        //    {
        //        if (wing)
        //        {
        //            wing.Lift = _roll / 100;
        //        }
        //    }
        //}
    }
}
