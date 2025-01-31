using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _maxSpeed, _rotationSpeed, _acceleration, _rotationInput;
    private float _speed, _accelerationInterpolator;
    private bool _isAccelerating;
    [SerializeField] private AnimationCurve _accelerationCurve, _decelerationCurve;
    void FixedUpdate()
    {
        if (_isAccelerating)
        { 
            _accelerationInterpolator += _acceleration;
        }
        else
        {
            _accelerationInterpolator -= _decelerationCurve.Evaluate(_accelerationInterpolator)* _acceleration; 
        }
        transform.eulerAngles+= Vector3.up * (_rotationSpeed*Time.fixedDeltaTime * _rotationInput);
        _accelerationInterpolator = Mathf.Clamp01(_accelerationInterpolator);
        _speed = _accelerationCurve.Evaluate(_accelerationInterpolator)*_maxSpeed;
        _rb.MovePosition(transform.position + transform.forward * (_speed * Time.fixedDeltaTime));
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _isAccelerating = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isAccelerating = false;
        }
        _rotationInput = Input.GetAxis("Horizontal");
         
        var xAngle = Mathf.Clamp(transform.eulerAngles.x+360, 320f, 400f);
        var yAngle = transform.eulerAngles.y;
        var zAngle = 0f;
        transform.eulerAngles = new Vector3(xAngle, yAngle, zAngle);
    }
}
