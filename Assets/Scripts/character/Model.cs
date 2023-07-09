using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model
{
    View _view;
    Rigidbody myRigidbody;
    Transform _transform;
    float _speed;
    Controls virtualStick;

    public Model(View view, Transform t, Rigidbody r, float speed, Controls controls)
    {
        _view = view;
        _transform = t;
        myRigidbody = r;
        _speed = speed;
        virtualStick = controls;
    }
    public void Movement(Vector3 simulatedVector)
    {
        _transform.LookAt(_transform.position + simulatedVector);
        myRigidbody.MovePosition(myRigidbody.position + _transform.forward * simulatedVector.magnitude * _speed * Time.deltaTime);
    }
}
