using System;
using System.Collections.Generic;
using UnityEngine;

// Class representing an item in the inventory
[Serializable]
public class Item
{
    public string itemName;          // Name of the item
    public string itemDescription;   // Description of the item
    public int itemRarity;           // Rarity of the item (1 to 5)

    public Item(string name, string description, int rarity)
    {
        itemName = name;
        itemDescription = description;
        itemRarity = rarity;
    }
}

// Class managing the player's inventory
public class Inventory : MonoBehaviour
{
    private List<Item> itemList = new List<Item>(); // List to hold items in the inventory

    
    public void AddItem(Item item)
    {
        itemList.Add(item);
        Debug.Log($"Added {item.itemName} to inventory.");
    }

    public void RemoveItem(Item item)
    {
        if (itemList.Contains(item))
        {
            itemList.Remove(item);
            Debug.Log($"Removed {item.itemName} from inventory.");
        }
        else
        {
            Debug.Log($"Item {item.itemName} not found in inventory.");
        }
    }

    public List<Item> GetItems()
    {
        return itemList;
    }
}