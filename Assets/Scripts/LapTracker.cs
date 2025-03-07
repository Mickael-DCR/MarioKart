using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapTracker : MonoBehaviour
{
    [SerializeField] private int _flags, _totalFlags, _laps, _totalLaps;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flag"))
        {
            _flags++;
            if (_flags >= _totalFlags)
            {
                _flags = 0;
                _laps++;
            }
        }
    }
    
    
}
