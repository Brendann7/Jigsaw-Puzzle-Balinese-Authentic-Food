using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Referensi")]
    public GameManager gameManager;
    public RectTransform targetPosition; // Pasangkan area tempat puzzle ini seharusnya berada (bagian warna hitam di kiri)
    public Canvas canvas; // Masukkan Canvas utamamu ke sini agar pergeseran mouse akurat

    [Header("Pengaturan Jarak Benar")]
    public float snapDistance = 50f; // Toleransi jarak agar puzzle otomatis menempel

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition; // Posisi awal puzzle di sebelah kanan
    private bool isLocked = false; // Menandakan puzzle sudah benar atau belum

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        
        // Menambahkan CanvasGroup otomatis jika belum ada (berguna agar puzzle tidak bentrok klik saat diseret)
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        startPosition = rectTransform.anchoredPosition; // Simpan posisi asal
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        AudioManager.instance.PlayDragPuzzle();
        if (isLocked) return; // Jika sudah di tempat yang benar, puzzle tidak bisa ditarik lagi

        // Pindahkan kepingan ini ke layer paling depan agar tidak tertutup puzzle lain
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isLocked) return;

        // Menggerakkan puzzle mengikuti mouse
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        AudioManager.instance.PlayDropPuzzle();
        if (isLocked) return;

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
}