using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("References")]
    public GameManager gameManager;
    public RectTransform targetPosition; // Area where the puzzle piece should end up
    public Canvas canvas; // Assign the main Canvas here

    [Header("Correct Distance Settings")]
    public float snapDistance = 50f; // Distance tolerance for the piece to auto-snap into place

    private RectTransform rectTransform;
    private RectTransform canvasRectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition; // Piece's original position on the right side
    private bool isLocked = false; // Whether the piece is already correctly placed

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        
        if (canvas != null)
        {
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }

        // Automatically add a CanvasGroup if one doesn't already exist
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        startPosition = rectTransform.anchoredPosition; // Store the original position
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isLocked) return; // If already correctly placed, ignore

        AudioManager.instance.PlayDragPuzzle();

        // Move this piece to the front-most layer so it isn't covered by other pieces
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isLocked) return;

        // Move the piece following the mouse/pointer
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        // Keep the piece within the Canvas bounds
        ClampToCanvas();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isLocked) return;

        AudioManager.instance.PlayDropPuzzle();
        canvasGroup.blocksRaycasts = true; // Re-enable click/raycast detection

        // Compute the distance between the piece's drop position and its correct target position
        float distance = Vector2.Distance(rectTransform.anchoredPosition, targetPosition.anchoredPosition);

        if (distance <= snapDistance)
        {
            // CORRECT: snap to the target position, lock it, and notify the GameManager
            rectTransform.anchoredPosition = targetPosition.anchoredPosition;
            isLocked = true;
            gameManager.AddPlacedPiece();
        }
        else
        {
            // INCORRECT: return the piece to its original position on the right
            rectTransform.anchoredPosition = startPosition;
        }
    }

    void ClampToCanvas()
    {
        if (canvasRectTransform == null) return;

        // Get the Canvas's bounding rect
        Rect canvasRect = canvasRectTransform.rect;
        
        // Get the puzzle piece's size (RectTransform)
        Vector2 sizeDelta = rectTransform.sizeDelta;
        Vector2 pivot = rectTransform.pivot;

        // Compute the min/max anchoredPosition bounds within the Canvas,
        // accounting for the piece's pivot so it doesn't go half off-screen
        float minX = canvasRect.xMin + (sizeDelta.x * pivot.x);
        float maxX = canvasRect.xMax - (sizeDelta.x * (1 - pivot.x));
        float minY = canvasRect.yMin + (sizeDelta.y * pivot.y);
        float maxY = canvasRect.yMax - (sizeDelta.y * (1 - pivot.y));

        Vector2 clampedPosition = rectTransform.anchoredPosition;

        // Clamp X and Y so the piece stays within the Canvas bounds
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);

        rectTransform.anchoredPosition = clampedPosition;
    }
}