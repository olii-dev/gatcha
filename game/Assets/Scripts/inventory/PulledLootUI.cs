using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PulledLootUI : MonoBehaviour
{
    public RectTransform contentPanel; // The Content object of your ScrollView
    public GameObject itemEntryPrefab; // Prefab for a single item row (Text or custom UI)

    private List<GameObject> spawnedEntries = new List<GameObject>();

    void Start()
    {
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

        var items = LootManager.pulledLoot;
        foreach (var loot in items)
        {
            GameObject entry = Instantiate(itemEntryPrefab, contentPanel);
            var tmp = entry.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
                tmp.text = loot;
            spawnedEntries.Add(entry);
        }
    }

    // Call this method from other scripts after adding loot
    public void OnLootAdded()
    {
        RefreshUI();
    }
}
