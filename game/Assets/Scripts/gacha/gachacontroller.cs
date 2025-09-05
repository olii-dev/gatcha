using UnityEngine;

public class GachaController : MonoBehaviour
{
    private WheelScroller wheelScroller;

    void Start()
    {
        wheelScroller = GetComponent<WheelScroller>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int panel = wheelScroller.CurrentIndex;

            switch (panel)
            {
                case 1:
                    RunGachaMachine1();
                    break;
                case 2:
                    RunGachaMachine2();
                    break;
                case 3:
                    RunGachaMachine3();
                    break;
                default:
                    Debug.Log("No gacha on this panel.");
                    break;
            }
        }
    }

    private void RunGachaMachine1()
    {
        Debug.Log("Gacha 1 result: " + RollLoot());
        // TODO: play animation here
    }

    private void RunGachaMachine2()
    {
        Debug.Log("Gacha 2 result: " + RollLoot());
        // TODO: play animation here
    }

    private void RunGachaMachine3()
    {
        Debug.Log("Gacha 3 result: " + RollLoot());
        // TODO: play animation here
    }

    private string RollLoot()
    {
        // Adjust these chances however you like (they must total 100)
        int roll = Random.Range(1, 101); // 1–100
        if (roll <= 60) return "Common";       // 60% chance
        if (roll <= 85) return "Rare";         // next 25%
        if (roll <= 97) return "Epic";         // next 12%
        return "Legendary";                    // last 3%
    }
}
