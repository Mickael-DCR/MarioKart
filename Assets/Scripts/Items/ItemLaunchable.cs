using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ItemLaunchable", menuName = "Items/Item Launchable")]
public class ItemLaunchable : Item
{
    public GameObject ItemPrefab;
    public override void Activation(PlayerItemManager player)
    {
        Instantiate(ItemPrefab, player.transform.position, player.transform.rotation);
    }
}
