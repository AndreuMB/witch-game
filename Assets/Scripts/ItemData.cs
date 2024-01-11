using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData
{
    public string itemName;
    public Item.ItemType itemType;
    public int originalPrice;
    public int stock;
    public bool consumable;
}
