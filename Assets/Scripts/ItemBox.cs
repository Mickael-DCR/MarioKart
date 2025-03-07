using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _meshRenderer;
    [SerializeField]
    private Collider _collider;
    [SerializeField]
    private float _waitBeforeRespawn = 1;
    private void OnTriggerEnter(Collider other)
    {
        PlayerItemManager playerItemManagerInContact = other.GetComponent<PlayerItemManager>();
        if(playerItemManagerInContact != null)
        {
            playerItemManagerInContact.GenerateItem();
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        _collider.enabled = false;
        _meshRenderer.enabled = false;
        yield return new WaitForSeconds(_waitBeforeRespawn);
        _collider.enabled = true;
        _meshRenderer.enabled = true;

    }
}
