using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInventory", menuName = "Player/Inventory")]
public class PlayerInventory : ScriptableObject
{
    public List<Item> items;
    public int maxSlots = 4;

    public bool AddItem(Item item)
    {
        if (items.Count < maxSlots)
        {
            items.Add(item);
            return true;
        }

        return false;
    }
    
    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }
    
    public void ClearInventory()
    {
        items.Clear();
    }
}

[CreateAssetMenu(fileName = "Item", menuName = "Player/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public bool isConsumable;
    public string description;
}