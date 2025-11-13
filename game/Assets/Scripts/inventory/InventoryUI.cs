using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [Header("Optional: on-screen controls HUD")]
    [Tooltip("Assign a TextMeshProUGUI element to display control hints. If empty, one will be created at runtime.")]
    public TextMeshProUGUI controlsText;
    // small config for runtime-created HUD
    public Vector2 controlsAnchorMin = new Vector2(0.01f, 0.01f);
    public Vector2 controlsAnchorMax = new Vector2(0.3f, 0.2f);
    public int controlsFontSize = 18;

    public Inventory inventory; // Assign in inspector or find at runtime
    public RectTransform contentPanel; // The Content object of your ScrollView
    public GameObject itemEntryPrefab; // Prefab for a single item row (Text or custom UI)

    private List<GameObject> spawnedEntries = new List<GameObject>();

    void Start()
    {
        if (inventory == null)
            inventory = FindObjectOfType<Inventory>();
        EnsureControlsHUD();
        RefreshUI();
    }

    void OnEnable()
    {
        RefreshUI();
    }

    void EnsureControlsHUD()
    {
        // If an Inspector-assigned text already exists, replace its content and return
        if (controlsText != null)
        {
            controlsText.text = GetDefaultControlsText();
            return;
        }

        // If a persistent HUD already exists from another scene, reuse it
        GameObject existing = GameObject.Find("ControlsHUD_Persistent");
        if (existing != null)
        {
            var existingTMP = existing.GetComponent<TextMeshProUGUI>();
            if (existingTMP != null)
            {
                controlsText = existingTMP;
                controlsText.text = GetDefaultControlsText();
                return;
            }
        }

        // Try to find an existing Canvas
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasGO = new GameObject("Canvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
        }

        // Create a container for controls and make it persistent across scenes
        GameObject controlsGO = new GameObject("ControlsHUD_Persistent");
        controlsGO.transform.SetParent(canvas.transform, false);
        DontDestroyOnLoad(controlsGO);

        RectTransform rt = controlsGO.AddComponent<RectTransform>();
        rt.anchorMin = controlsAnchorMin;
        rt.anchorMax = controlsAnchorMax;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        // Add TextMeshProUGUI
        controlsText = controlsGO.AddComponent<TextMeshProUGUI>();
        controlsText.raycastTarget = false;
        controlsText.fontSize = controlsFontSize;
        controlsText.alignment = TextAlignmentOptions.TopLeft;
        controlsText.color = Color.white;
        controlsText.text = GetDefaultControlsText();

        // Add small shadow for readability if TMPro has it
        var shadow = controlsGO.AddComponent<Shadow>();
        shadow.effectColor = new Color(0, 0, 0, 0.6f);
        shadow.effectDistance = new Vector2(1f, -1f);
    }

    string GetDefaultControlsText()
    {
        // User-requested wording
        return "Controls:\n- Tab is to switch scenes\n- B is to switch items\n- Space is to collect gold and spend gold";
    }

    public void RefreshUI()
    {
        Debug.Log("Refreshing Inventory UI");
        // Clear old entries
        foreach (var go in spawnedEntries)
            Destroy(go);
        spawnedEntries.Clear();

        if (inventory == null) return;
        var items = inventory.GetItems();
        foreach (var item in items)
        {
            Debug.Log($"New item: {item.itemName}");
            GameObject entry = Instantiate(itemEntryPrefab, contentPanel);
            var tmp = entry.GetComponent<TextMeshProUGUI>();
            //if (tmp != null){
                tmp.text = item.itemName;
                Debug.Log($"Adding item to UI: {item.itemName} to object {entry.name}");
            //}
                
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
