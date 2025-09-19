using UnityEngine;

public class ShowPulledLoot : MonoBehaviour
{
    void Start()
    {
        if (LootManager.pulledLoot.Count == 0)
        {
            Debug.Log("No loot has been pulled yet.");
            return;
        }

        foreach (string loot in LootManager.pulledLoot)
        {
            Debug.Log("Player pulled: " + loot);
        }

        // Log when done
        Debug.Log("All pulled loot has been displayed in the console.");
    }
}
