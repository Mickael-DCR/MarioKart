using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LapTracker : MonoBehaviour
{
    private int _lapNumber;
    private List<Checkpoint> _checkpoints;
    private int _numberOfCheckpoints;
    [SerializeField] private TextMeshProUGUI _lapNumberText;
    [SerializeField] private Image _rankImage;
    [SerializeField] private GameObject _finishImage;


    private void Start()
    {
        _lapNumberText.text= ++_lapNumber+"/3";
        _numberOfCheckpoints = FindObjectsByType<Checkpoint>(FindObjectsSortMode.None).Length; 
        _checkpoints = new List<Checkpoint>();
    }

    public void AddCheckpoint(Checkpoint checkPointToAdd)
    {
        if(checkPointToAdd.isFinishLine)
        {
            FinishLap();
        }

        if(_checkpoints.Contains(checkPointToAdd) == false)
        {
            _checkpoints.Add(checkPointToAdd);
        }
    }

    private void FinishLap()
    {
        if (_checkpoints.Count > _numberOfCheckpoints/2)
        {
            _lapNumberText.text= ++_lapNumber+"/3";
            _checkpoints.Clear();
            if(_lapNumber>3)
            {
                _lapNumberText.text= "3/3";
                _finishImage.gameObject.SetActive(true);
            }
        }
    }
    
    
}
