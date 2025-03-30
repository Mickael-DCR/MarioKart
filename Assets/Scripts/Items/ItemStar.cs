using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemStar", menuName = "Items/Star")]
public class ItemStar : Item
{
    public override void Activation(PlayerItemManager player)
    {
        player.CarController.IsInvincible = true;
        player.CarController.Turbo();
    }
}
