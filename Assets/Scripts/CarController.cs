using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private string _accelerationInput = "Accelerate", _steeringInput = "Horizontal";
    [SerializeField] private Rigidbody _rb;
    [Header("Speed Settings")]
    [SerializeField] private float _baseMaxSpeed, _speedMaxTurbo, _rotationSpeed, _acceleration, _rotationInput, _boostDuration;
    private float _speed, _accelerationInterpolator;
    private bool _isAccelerating, _isBoosting;
    [SerializeField] private AnimationCurve _accelerationCurve, _decelerationCurve;
    [Header("Terrain Modification")]
    [SerializeField] private float _terrainSpeedVariator;
    [SerializeField] private float _raycastDistance;
    [SerializeField] private LayerMask _outerTerrainMask, _boostTerrainMask;

    public bool IsHindered;
    public bool IsInvincible;
    
    [Header("RocketStart")]
    [SerializeField] private RocketStart _rocketStart;
    private float _rocketStartTime;
    

    private void FixedUpdate()
    {
        if (!_rocketStart.GameStarted)
        {
        }
        else
        {
            if (!_rocketStart.IsRocketStartDone && _rocketStartTime is <= 2 and > 0)
            {
                Turbo();
                _rocketStart.IsRocketStartDone = true;
            }
            if (IsInvincible || _isAccelerating && !IsHindered)
            { 
                _accelerationInterpolator += _acceleration;
            }
            else
            {
                _accelerationInterpolator -= _decelerationCurve.Evaluate(_accelerationInterpolator)* _acceleration; 
            }
        
            _accelerationInterpolator = Mathf.Clamp01(_accelerationInterpolator);
        
            if(_isBoosting)
            {
                _speed = _speedMaxTurbo;
            }
            else
            {
                _speed = _accelerationCurve.Evaluate(_accelerationInterpolator)*_baseMaxSpeed*_terrainSpeedVariator;
            }
        
            transform.eulerAngles+= Vector3.up * (_rotationSpeed*Time.fixedDeltaTime * _rotationInput);
            _rb.MovePosition(transform.position + transform.forward * (_terrainSpeedVariator* _speed * Time.fixedDeltaTime));
        }
    }
    
    void Update()
    {
        if (_rocketStart.CountDownRunning && Input.GetButton(_accelerationInput))
        {
            _rocketStartTime += Time.deltaTime;
            Debug.Log(_rocketStartTime);
        }
        else
        {
            TerrainModifier();
            OnBoostingPad();
            if(Input.GetButtonDown(_accelerationInput))
            {
                _isAccelerating = true;
            }

            if (Input.GetButtonUp(_accelerationInput))
            {
                _isAccelerating = false;
            }
            _rotationInput = Input.GetAxis(_steeringInput);
        }
    }
    // Gere la vitesse en fonction du terrain touché
    private void TerrainModifier()
    {
        if (Physics.Raycast(transform.position, transform.up * -1, out var info, _raycastDistance, _outerTerrainMask))
        {
            Terrains terrainBellow = info.transform.GetComponent<Terrains>();
            if (terrainBellow != null)
            {
                _terrainSpeedVariator = terrainBellow.SpeedVariator;
            }
        }
        else
        {
            _terrainSpeedVariator = 1;
        }
    }
    public void Turbo()
    {
        if (!_isBoosting)
        {
            StartCoroutine(Turboroutine());
        }
    }

    private IEnumerator Turboroutine()
    {
        _isBoosting = true;
        yield return new WaitForSeconds(_boostDuration);
        _isBoosting = false;
    }

    private void OnBoostingPad()
    {
        if (Physics.Raycast(transform.position, transform.up * -1, 0.1f, _boostTerrainMask))
        {
            Turbo();
        }
    }
    
    
}
