using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LapTracker : MonoBehaviour
{
    [SerializeField] private int _totalFlags, _laps, _totalLaps;
    [SerializeField] private List<GameObject> _flagsList;
    [SerializeField] private TextMeshProUGUI _lapsText;

    private void Start()
    {
        _lapsText.text = _laps+"/"+_totalLaps;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flag"))
        {
            _flagsList.Add(other.gameObject);
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Finish"))
        {
            
            if (_flagsList.Count == _totalFlags)
            {
                for (int i = 0; i < _flagsList.Count; i++)
                {
                    _flagsList[i].SetActive(true);
                }
                
                if (_laps >= _totalLaps)
                {
                    Debug.Log("Race Complete!");
                }
                else
                {
                    _flagsList.Clear();
                    _lapsText.text = ++_laps+"/"+_totalLaps;
                }
            }
        }
    }

    
}
