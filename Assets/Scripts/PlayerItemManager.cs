using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class PlayerItemManager : MonoBehaviour
{
    [SerializeField] private List<Item> _itemList;
    [SerializeField] private Item _currentItem;
    [SerializeField] private Image _itemImage;
    [SerializeField] private int  _numberOfItemUse;
    public CarController CarController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            UseItem();
        }
    }

   

    public void GenerateItem()
    {
        if (_currentItem == null)
        {
            _currentItem = _itemList[Random.Range(0,_itemList.Count)];
            _itemImage.sprite = _currentItem.ItemSprite;
            _numberOfItemUse = _currentItem.ItemUseCount;
        }
        
    }

    public void UseItem()
    {
        if (_currentItem != null)
        {
            _currentItem.Activation(this);
            _numberOfItemUse--;
            if (_numberOfItemUse <= 0)
            {
                _currentItem = null;
                _itemImage.sprite = null;
            }
        }
    }
}
