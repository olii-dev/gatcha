using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if TMP_PRESENT
using TMPro;
#endif

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory; // Assign in inspector or find at runtime
    public RectTransform contentPanel; // The Content object of your ScrollView
    public GameObject itemEntryPrefab; // Prefab for a single item row (Text or custom UI)

    private List<GameObject> spawnedEntries = new List<GameObject>();

    void Start()
    {
        if (inventory == null)
            inventory = FindObjectOfType<Inventory>();
        RefreshUI();
    }

    void OnEnable()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        // Clear old entries
        foreach (var go in spawnedEntries)
            Destroy(go);
        spawnedEntries.Clear();

        if (inventory == null) return;
        var items = inventory.GetItems();
        foreach (var item in items)
        {
            GameObject entry = Instantiate(itemEntryPrefab, contentPanel);
            // Try TextMeshProUGUI first, then fallback to Text
#if TMP_PRESENT
            var tmp = entry.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if (tmp != null)
                tmp.text = $"{item.itemName} (Rarity: {item.itemRarity})";
            else
#endif
            {
                var text = entry.GetComponentInChildren<Text>();
                if (text != null)
                    text.text = $"{item.itemName} (Rarity: {item.itemRarity})";
            }
            spawnedEntries.Add(entry);
        }
    }

    // Call this method from other scripts after adding loot to inventory
    public void OnLootAdded()
    {
        RefreshUI();
    }

    // (Auto-refresh on inventory change removed for simplicity)
}
