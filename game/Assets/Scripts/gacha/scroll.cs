using UnityEngine;

public class WheelScroller : MonoBehaviour
{
    public RectTransform container;   // Parent holding all 4 Wheel panels
    public float panelWidth = 1920f;  // Width of each panel
    public float slideSpeed = 1000f;  // Sliding speed in units per second

    private int currentIndex = 0;     
    private int totalPanels = 4;      
    private Vector2 targetPos;        
    private bool isMoving = false;
public int CurrentIndex
    {
       get { return currentIndex; }
    }

    void Start()
    {
        targetPos = container.anchoredPosition;
    }

    void Update()
    {
        // Move to next panel on B key if not moving
        if (Input.GetKeyDown(KeyCode.B) && !isMoving)
        {
            NextPanel();
        }

        // Smoothly move container toward target
        if (isMoving)
        {
            container.anchoredPosition = Vector2.MoveTowards(
                container.anchoredPosition,
                targetPos,
                slideSpeed * Time.deltaTime
            );

            // Check if target reached
            if (Vector2.Distance(container.anchoredPosition, targetPos) < 0.01f)
            {
                container.anchoredPosition = targetPos;

                // Auto-loop from last panel
                if (currentIndex >= totalPanels)
                {
                    // Snap instantly to first panel
                    container.anchoredPosition = Vector2.zero;
                    currentIndex = 0;
                    targetPos = container.anchoredPosition;
                    // Start sliding automatically to the next panel
                    NextPanel();
                }
                else
                {
                    isMoving = false;
                }
            }
        }
    }

    private void NextPanel()
    {
        currentIndex++;
        targetPos = new Vector2(-Mathf.Min(currentIndex, totalPanels - 1) * panelWidth, 0);
        isMoving = true;
    }
}
