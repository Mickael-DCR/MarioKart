using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [Header("Speed Settings")]
    [SerializeField] private float _baseMaxSpeed, _maxSpeed, _rotationSpeed, _acceleration, _rotationInput;
    private float _speed, _accelerationInterpolator;
    private bool _isAccelerating;
    public bool IsBoosting;
    [SerializeField] private AnimationCurve _accelerationCurve, _decelerationCurve;
    [Header("Terrain Modification")]
    private float _terrainSpeedVariator;
    [SerializeField] private float _raycastDistance;
    [SerializeField] private LayerMask _layerMask;
    
    private void Start()
    {
        _baseMaxSpeed = _maxSpeed;
    }

    public void ResetMaxSpeed()
    {
        _maxSpeed = _baseMaxSpeed;
    }
    public void SetMaxSpeed(float newMaxSpeed)
    {
        _maxSpeed = newMaxSpeed;
    }
    private void FixedUpdate()
    {
        if (_isAccelerating || IsBoosting)
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
        _rb.MovePosition(transform.position + transform.forward * (_terrainSpeedVariator* _speed * Time.fixedDeltaTime));
    }
    void Update()
    {
        TerrainModifier();
        Debug.Log(" transform.eulerAngles.x : " + transform.eulerAngles.x);
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _isAccelerating = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isAccelerating = false;
        }
        _rotationInput = Input.GetAxis("Horizontal");
    }
    // Gere la vitesse en fonction du terrain touché
    private void TerrainModifier()
    {
        if (Physics.Raycast(transform.position, transform.up * -1, out var info, _raycastDistance, _layerMask))
        {
            Terrain terrainBellow = info.transform.GetComponent<Terrain>();
            if (terrainBellow != null)
            {
                _terrainSpeedVariator = terrainBellow.SpeedVariator;
            }
            else
            {
                _terrainSpeedVariator = 1;
            }
        }
        else
        {
            _terrainSpeedVariator = 1;
        }
    }
    //
    private void BalanceVehicle()
    {
        transform.Rotate()
    }
}
