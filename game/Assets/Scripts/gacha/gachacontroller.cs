using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GachaController : MonoBehaviour
{
    private WheelScroller wheelScroller;

    [Header("UI References")]
    public GameObject resultPanel;        
    public TextMeshProUGUI resultText;    
    public Image resultIcon;              

    [Header("Loot Tables")]
    public LootItem[] gacha1Loot;   
    public LootItem[] gacha2Loot;   
    public LootItem[] gacha3Loot;   
    public LootItem[] gacha4Loot;   // added for 4th panel

    [Header("Gacha Costs")]
    public int panel1Cost = 10;
    public int panel2Cost = 20;
    public int panel3Cost = 30;
    public int panel4Cost = 40;   // added for 4th panel

    private bool showingResult = false;

    void Start()
    {
        wheelScroller = GetComponent<WheelScroller>();
        if (resultPanel != null)
            resultPanel.SetActive(false); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!showingResult)
            {
                int panel = wheelScroller.CurrentIndex;

                // Determine cost for this panel
                int cost = GetPanelCost(panel);

                // Check if player has enough gold
                if (NewMonoBehaviourScript.gold >= cost)
                {
                    NewMonoBehaviourScript.gold -= cost;

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
                    Debug.Log("Not enough gold to roll this panel!");
                }
            }
            else
            {
                if (resultPanel != null)
                    resultPanel.SetActive(false);

                showingResult = false;
            }
        }
    }

    private int GetPanelCost(int panel)
    {
        switch(panel)
        {
            case 0: return panel1Cost;
            case 1: return panel2Cost;
            case 2: return panel3Cost;
            case 3: return panel4Cost;   // added case for 4th panel
            default: return 0;
        }
    }

    private LootItem RollFromPanel(int panel)
    {
        LootItem[] table = null;

        switch(panel)
        {
            case 0: table = gacha1Loot; break;
            case 1: table = gacha2Loot; break;
            case 2: table = gacha3Loot; break;
            case 3: table = gacha4Loot; break;  // added case for 4th panel
            default:
                Debug.Log("No gacha for this panel.");
                return null;
        }

        if (table == null || table.Length == 0)
        {
            Debug.LogWarning($"Panel {panel} has no loot items!");
            return null;
        }

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

