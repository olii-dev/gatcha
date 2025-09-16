using UnityEngine;

public class VerticalScroller : MonoBehaviour
{
    public RectTransform container; // The object to scroll vertically (e.g., your square)
    public float scrollAmount = 200f; // Amount to scroll per step (in pixels/units)
    public float slideSpeed = 1000f; // Sliding speed in units per second
    public float minY = 0f; // Top limit (adjust as needed)
    public float maxY = -800f; // Bottom limit (adjust as needed)

    private Vector2 targetPos;
    private bool isMoving = false;
    private int direction = -1; // -1 = down, 1 = up

    void Start()
    {
        targetPos = container.anchoredPosition;
    }

    void Update()
    {
        // Scroll on B key if not moving
        if (Input.GetKeyDown(KeyCode.B) && !isMoving)
        {
            Scroll();
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

    private void Scroll()
    {
        float nextY = container.anchoredPosition.y + direction * scrollAmount;
        // Clamp and reverse direction if at bounds
        if (nextY < maxY)
        {
            nextY = maxY;
            direction = 1; // Reverse to up
        }
        else if (nextY > minY)
        {
            nextY = minY;
            direction = -1; // Reverse to down
        }
        targetPos = new Vector2(container.anchoredPosition.x, nextY);
        isMoving = true;
    }
}
