using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RocketStart : MonoBehaviour
{
    [SerializeField] private List<GameObject> _startImages;
    public bool GameStarted;
    public bool CountDownRunning;
    public bool IsRocketStartDone;
    private void Start()
    {
        StartCoroutine(GameStart());
    }
    private IEnumerator GameStart()
    {
        CountDownRunning = true;
        _startImages[0].SetActive(true);
        yield return new WaitForSeconds(1);
        _startImages[0].SetActive(false);
        _startImages[1].SetActive(true);
        yield return new WaitForSeconds(1);
        _startImages[1].SetActive(false);
        _startImages[2].SetActive(true);
        yield return new WaitForSeconds(1);
        CountDownRunning = false;
        _startImages[2].SetActive(false);
        _startImages[3].SetActive(true);
        GameStarted = true;
        yield return new WaitForSeconds(1);
        _startImages[3].SetActive(false);
        
    }
}
