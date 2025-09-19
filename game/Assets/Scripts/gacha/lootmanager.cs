using System.Collections.Generic;
using UnityEngine;

public static class LootManager
{
    public static List<string> pulledLoot = new List<string>();

    public static void AddLoot(string lootName)
    {
        pulledLoot.Add(lootName);
        Debug.Log($"Added {lootName} to pulled loot list!");
    }

    public static void ClearLoot()
    {
        pulledLoot.Clear();
    }
}
