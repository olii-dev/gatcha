using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class GachaController : MonoBehaviour
{
    private WheelScroller wheelScroller;

    [Header("UI References")]
    
    
    [Header("UI References")]
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public Transform lootSpawnPoint; // ADD THIS LINE


    [Header("Animation")]
    public Animator gachaAnimator;            // attach your gacha Animator
    public string rollTriggerName = "Roll";   // Animator trigger name

    [Header("Loot Tables")]
    public LootItem[] gacha1Loot;
    public LootItem[] gacha2Loot;
    public LootItem[] gacha3Loot;
    public LootItem[] gacha4Loot;

    [Header("Gacha Costs")]
    public int panel1Cost = 10;
    public int panel2Cost = 20;
    public int panel3Cost = 30;
    public int panel4Cost = 40;

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
                int cost = GetPanelCost(panel);

                if (NewMonoBehaviourScript.gold >= cost)
                {
                    NewMonoBehaviourScript.gold -= cost;

                    // Play the roll animation
                    if (gachaAnimator != null)
                        gachaAnimator.SetTrigger(rollTriggerName);

                    // Show result after animation
                    StartCoroutine(ShowGachaResult(panel));
                    showingResult = true;
                }
                else
                {
                    Debug.Log("Not enough gold to roll this panel!");
                }
            }
            else
            {
                // Hide previous result
                if (resultPanel != null)
                    resultPanel.SetActive(false);

                showingResult = false;
            }
        }
    }

    private int GetPanelCost(int panel)
    {
        switch (panel)
        {
            case 0: return panel1Cost;
            case 1: return panel2Cost;
            case 2: return panel3Cost;
            case 3: return panel4Cost;
            default: return 0;
        }
    }

    private IEnumerator ShowGachaResult(int panel)
    {
        // Wait for the animation to finish (adjust time to your animation length)
        yield return new WaitForSeconds(1.0f);

        LootItem loot = RollFromPanel(panel);

        if (loot != null && resultPanel != null && resultText != null)
        {
            resultPanel.SetActive(true);
            resultText.text = loot.itemName;

            // Spawn prefab as child of resultPanel
            if (loot.prefab != null)
            {
                // Clear previous prefab first
                // Clear previous loot only
                foreach (Transform child in lootSpawnPoint)
                Destroy(child.gameObject);

                // Spawn new loot prefab
                if (loot.prefab != null)
                {
                GameObject go = Instantiate(loot.prefab, lootSpawnPoint);
                go.transform.localPosition = Vector3.zero; // adjust if needed
                go.transform.localScale = Vector3.one;
                }

            }
        }
    }

    private LootItem RollFromPanel(int panel)
    {
        LootItem[] table = null;

        switch (panel)
        {
            case 0: table = gacha1Loot; break;
            case 1: table = gacha2Loot; break;
            case 2: table = gacha3Loot; break;
            case 3: table = gacha4Loot; break;
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
        public GameObject prefab; // use prefabs instead of sprites
        [Range(1, 100)]
        public int weight = 10;
    }
 
}
