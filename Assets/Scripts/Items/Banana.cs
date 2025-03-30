using System.Collections;using UnityEngine;

public class Banana : MonoBehaviour
{
    private Collider _collider;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private float _hinderTime;
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           StartCoroutine(HinderPlayer(other.GetComponent<CarController>()));
        }
    }


    private IEnumerator HinderPlayer(CarController carControllerInContact)
    {
        _collider.enabled = false;
        _meshRenderer.enabled = false;
        carControllerInContact.IsHindered = true;
        yield return new WaitForSeconds(_hinderTime);
        carControllerInContact.IsHindered = false;
        Debug.Log("destroyyyyy");
        Destroy(gameObject);
    }

    
}