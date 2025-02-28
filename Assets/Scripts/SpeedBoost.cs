using System;
using System.Collections;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private float MaxSpeedBoost = 20, BoostTime;
    private CarController _carController;
    
    private void Awake()
    {
        _carController = FindObjectOfType<CarController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Boost());
        }       
    }

    private IEnumerator Boost()
    {
        _carController.SetMaxSpeed(MaxSpeedBoost);
        _carController.IsBoosting = true;
        yield return new WaitForSeconds(BoostTime);
        _carController.IsBoosting = false;
        _carController.ResetMaxSpeed();
        Destroy(this.gameObject);
    }
}
