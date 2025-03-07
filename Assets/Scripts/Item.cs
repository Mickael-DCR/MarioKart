using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public Sprite ItemSprite;
    public string ItemName;
    public int ItemUseCount = 1;

    public virtual void Activation(PlayerItemManager player)
    {
        
    }
    
}
