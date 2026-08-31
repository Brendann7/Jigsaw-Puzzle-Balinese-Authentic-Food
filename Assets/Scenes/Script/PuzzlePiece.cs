using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Referensi")]
    public GameManager gameManager;
    public RectTransform targetPosition; // Area tempat puzzle seharusnya berada
    public Canvas canvas; // Masukkan Canvas utama ke sini

    [Header("Pengaturan Jarak Benar")]
    public float snapDistance = 50f; // Toleransi jarak agar puzzle otomatis menempel

    private RectTransform rectTransform;
    private RectTransform canvasRectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition; // Posisi awal puzzle di sebelah kanan
    private bool isLocked = false; // Menandakan puzzle sudah benar atau belum

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        
        if (canvas != null)
        {
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }

        // Menambahkan CanvasGroup otomatis jika belum ada
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        startPosition = rectTransform.anchoredPosition; // Simpan posisi asal
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isLocked) return; // Jika sudah di tempat yang benar, abaikan

        AudioManager.instance.PlayDragPuzzle();

        // Pindahkan kepingan ini ke layer paling depan agar tidak tertutup puzzle lain
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isLocked) return;

        // Menggerakkan puzzle mengikuti mouse
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        // Batasi posisi agar tidak keluar dari area Canvas
        ClampToCanvas();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isLocked) return;

        AudioManager.instance.PlayDropPuzzle();
        canvasGroup.blocksRaycasts = true; // Nyalakan lagi deteksi klik

        // Hitung jarak antara posisi puzzle saat dilepas dengan posisi target benarnya
        float distance = Vector2.Distance(rectTransform.anchoredPosition, targetPosition.anchoredPosition);

        if (distance <= snapDistance)
        {
            // JIKA BENAR: Tempelkan ke tempat target, kunci, dan beri tahu GameManager
            rectTransform.anchoredPosition = targetPosition.anchoredPosition;
            isLocked = true;
            gameManager.AddPlacedPiece();
        }
        else
        {
            // JIKA SALAH: Kembalikan kepingan ke tempat semula di kanan
            rectTransform.anchoredPosition = startPosition;
        }
    }

    void ClampToCanvas()
    {
        if (canvasRectTransform == null) return;

        // Ambil batas area (rect) dari Canvas
        Rect canvasRect = canvasRectTransform.rect;
        
        // Ambil ukuran kepingan puzzle (RectTransform)
        Vector2 sizeDelta = rectTransform.sizeDelta;
        Vector2 pivot = rectTransform.pivot;

        // Hitung batas minimal dan maksimal anchoredPosition di dalam Canvas
        // Mempertimbangkan pivot kepingan puzzle agar tidak setengah keluar layar
        float minX = canvasRect.xMin + (sizeDelta.x * pivot.x);
        float maxX = canvasRect.xMax - (sizeDelta.x * (1 - pivot.x));
        float minY = canvasRect.yMin + (sizeDelta.y * pivot.y);
        float maxY = canvasRect.yMax - (sizeDelta.y * (1 - pivot.y));

        Vector2 clampedPosition = rectTransform.anchoredPosition;

        // Kunci posisi X dan Y agar tetap di dalam batas Canvas
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);

        rectTransform.anchoredPosition = clampedPosition;
    }
}