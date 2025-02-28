using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemBox : MonoBehaviour
{
    [SerializeField] private WeightedList<GameObject> _items;
    [SerializeField] private Transform _itemContainer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(_items.GetRandomElement(), _itemContainer);
            Destroy(gameObject);
        }
    }
}
