using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite icon;
    public int dropQuantity = 3;
    public int maxStackSize = 1;
}
