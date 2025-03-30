using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ItemLaunchable", menuName = "Items/Item Launchable")]
public class ItemLaunchable : Item
{
    [SerializeField] private GameObject _itemPrefab;
    public override void Activation(PlayerItemManager player)
    {
        Instantiate(_itemPrefab, player.transform.position+player.transform.forward*-2+player.transform.up*0.25f, player.transform.rotation);
    }
}
