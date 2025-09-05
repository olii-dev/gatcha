using UnityEngine;

public class GachaController : MonoBehaviour
{
    public WheelScroller wheelScroller;  // Reference to your scroller
    public GachaPanel[] panels;          // Assign panels in same order as WheelScroller

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int currentIndex = wheelScroller.CurrentIndex;
            if (currentIndex >= 0 && currentIndex < panels.Length)
            {
                panels[currentIndex].TriggerGacha();
            }
        }
    }
}
