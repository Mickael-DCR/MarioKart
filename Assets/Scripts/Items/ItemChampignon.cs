using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemChampignon", menuName = "Items/Champignon")]
public class ItemChampignon : Item
{
    public override void Activation(PlayerItemManager player)
    {
        player.CarController.Turbo();
    }
}
