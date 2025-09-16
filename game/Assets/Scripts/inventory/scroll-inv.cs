using UnityEngine;

public class VerticalScroller : MonoBehaviour
{
    public RectTransform container; // The object to scroll vertically (e.g., your square)
    public float scrollAmount = 200f; // Amount to scroll per step (in pixels/units)
    public float slideSpeed = 1000f; // Sliding speed in units per second

    private Vector2 targetPos;
    private bool isMoving = false;

    void Start()
    {
        targetPos = container.anchoredPosition;
    }

    void Update()
    {
        // Scroll up on B key if not moving
        if (Input.GetKeyDown(KeyCode.B) && !isMoving)
        {
            ScrollUp();
        }
        // Scroll down on N key if not moving
        if (Input.GetKeyDown(KeyCode.N) && !isMoving)
        {
            ScrollDown();
        }

        // Smoothly move container toward target
        if (isMoving)
        {
            container.anchoredPosition = Vector2.MoveTowards(
                container.anchoredPosition,
                targetPos,
                slideSpeed * Time.deltaTime
            );

            if (Vector2.Distance(container.anchoredPosition, targetPos) < 0.01f)
            {
                container.anchoredPosition = targetPos;
                isMoving = false;
            }
        }
    }

    private void ScrollUp()
    {
        targetPos = container.anchoredPosition + new Vector2(0, scrollAmount);
        isMoving = true;
    }

    private void ScrollDown()
    {
        targetPos = container.anchoredPosition - new Vector2(0, scrollAmount);
        isMoving = true;
    }
}
