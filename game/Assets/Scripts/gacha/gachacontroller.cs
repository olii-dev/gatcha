using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GachaController : MonoBehaviour
{
    private WheelScroller wheelScroller;

    [Header("UI References")]
    public GameObject resultPanel;        // Panel that shows result
    public TextMeshProUGUI resultText;    // Text field for result
    public Image resultIcon;              // Icon field for result

    [Header("Loot Tables")]
    public LootItem[] gacha1Loot;   // Panel 1 loot
    public LootItem[] gacha2Loot;   // Panel 2 loot
    public LootItem[] gacha3Loot;   // Panel 3 loot

    [Header("Gacha Settings")]
    public int rollCost = 10;   // cost of rolling (editable in Inspector)

    private bool showingResult = false;

    void Start()
    {
        wheelScroller = GetComponent<WheelScroller>();

        if (resultPanel != null)
            resultPanel.SetActive(false); // hide on start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!showingResult)
            {
                // check if player has enough gold
                if (NewMonoBehaviourScript.gold >= rollCost)
                {
                    // spend gold
                    NewMonoBehaviourScript.gold -= rollCost;

                    // roll loot based on active panel
                    int panel = wheelScroller.CurrentIndex;
                    LootItem loot = RollFromPanel(panel);

                    if (loot != null && resultPanel != null && resultText != null)
                    {
                        resultPanel.SetActive(true);
                        resultText.text = loot.itemName;

                        if (loot.icon != null && resultIcon != null)
                        {
                            resultIcon.sprite = loot.icon;
                            resultIcon.enabled = true;
                        }
                        else if (resultIcon != null)
                        {
                            resultIcon.enabled = false;
                        }
                    }

                    showingResult = true;
                }
                else
                {
                    Debug.Log("Not enough gold to roll!");
                }
            }
            else
            {
                // hide UI
                if (resultPanel != null)
                    resultPanel.SetActive(false);

                showingResult = false;
            }
        }
    }

    private LootItem RollFromPanel(int panel)
    {
        LootItem[] table = null;

        switch (panel)
        {
            case 1: table = gacha1Loot; break;
            case 2: table = gacha2Loot; break;
            case 3: table = gacha3Loot; break;
            default:
                Debug.Log("No gacha for this panel.");
                return null;
        }

        if (table == null || table.Length == 0)
        {
            Debug.LogWarning($"Panel {panel} has no loot items!");
            return null;
        }

        // Calculate total weight
        int totalWeight = 0;
        foreach (var item in table) totalWeight += item.weight;

        int roll = Random.Range(0, totalWeight);
        int cumulative = 0;

        foreach (var item in table)
        {
            cumulative += item.weight;
            if (roll < cumulative)
            {
                Debug.Log($"Panel {panel} rolled: {item.itemName}");
                return item;
            }
        }

        return null;
    }

    [System.Serializable]
    public class LootItem
    {
        public string itemName;
        public Sprite icon;
        [Range(1, 100)]
        public int weight = 10;
    }
}
