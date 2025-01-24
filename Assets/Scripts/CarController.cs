using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _acceleration;
    private float _;

    void FixedUpdate()
    {
        if(Input.GetButton("Jump"))
        {
            _acceleration = Mathf.Clamp(_acceleration +);
        } 
        _rb.MovePosition(transform.position + transform.forward * (_speed * Time.fixedDeltaTime));
    }
    void Update()
    {
        if(Input.GetAxisRaw("Horizontal")>0)
        {
            transform.eulerAngles+= Vector3.up * (_rotationSpeed*Time.deltaTime);
        } 
        if(Input.GetAxisRaw("Horizontal")<0)
        {
            transform.eulerAngles+= Vector3.down * (_rotationSpeed*Time.deltaTime);        
        }   
        
        var xAngle = Mathf.Clamp(transform.eulerAngles.x+360, 320f, 400f);
        var yAngle = transform.eulerAngles.y;
        var zAngle = 0f;
        transform.eulerAngles = new Vector3(xAngle, yAngle, zAngle);
        _speed = Mathf.Lerp(_acceleration, Time.deltaTime, _acceleration*Time.deltaTime);
        _speed = Mathf.Clamp(_speed+_acceleration, 0, _maxSpeed);
    }
}
