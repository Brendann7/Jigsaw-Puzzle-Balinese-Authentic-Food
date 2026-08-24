using UnityEngine;
using UnityEngine.UI; // Gunakan TMPro jika kamu pakai TextMeshPro
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Popups")]
    public GameObject popupInstruction;
    public GameObject popupScore;
    public GameObject popupInformation;

    [Header("Game UI & Timer")]
    public Text timerText; // Teks timer di atas tengah
    public Text finalTimeText; // Teks "USER TIME:" di popup score

    [Header("Puzzle Settings")]
    public int totalPieces = 10; // Ubah sesuai jumlah potongan puzzle kamu
    private int placedPieces = 0;

    [Header("Score Stars (Bintang)")]
    public GameObject[] stars; // Masukkan 3 gambar bintang dari Popup_Score ke sini

    private float currentTime = 0f;
    private bool isTimerRunning = false;

    void Start()
    {
        // Alur pertama: Munculkan Instruction, sembunyikan yang lain
        popupInstruction.SetActive(true);
        popupScore.SetActive(false);
        popupInformation.SetActive(false);

        isTimerRunning = false;
        currentTime = 0f;
    }

    void Update()
    {
        // Jika timer berjalan, hitung waktu
        if (isTimerRunning)
        {
            currentTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // --- FUNGSI UNTUK TOMBOL-TOMBOL ---

    // Pasang di tombol "NEXT" pada Popup_Instruction
    public void StartGame()
    {
        popupInstruction.SetActive(false);
        isTimerRunning = true; // Mulai timer
    }

    // Pasang di tombol "NEXT" pada Popup_Score
    public void ShowInformation()
    {
        popupScore.SetActive(false);
        popupInformation.SetActive(true);
    }

    // Pasang di tombol "GOT IT" pada Popup_Information
    public void GoToLevelSelection()
    {
        // Pastikan nama scene sesuai dengan yang ada di Build Settings
        SceneManager.LoadScene("level selection");
    }

    // --- FUNGSI LOGIKA PUZZLE ---

    // Akan dipanggil otomatis oleh potongan puzzle yang benar letaknya
    public void AddPlacedPiece()
    {
        placedPieces++;

        // Cek apakah semua puzzle sudah terpasang
        if (placedPieces >= totalPieces)
        {
            FinishGame();
        }
    }

    void FinishGame()
    {
        isTimerRunning = false; // Hentikan waktu
        popupScore.SetActive(true); // Munculkan popup score

        // Tampilkan waktu akhir pengguna di popup score
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        finalTimeText.text = "USER TIME : " + string.Format("{0:00}:{1:00}", minutes, seconds);

        CalculateStars();
    }

    void CalculateStars()
    {
        // Matikan semua bintang terlebih dahulu
        foreach (GameObject star in stars)
        {
            star.SetActive(false);
        }

        // Kalkulasi berdasarkan waktu (currentTime dalam detik)
        if (currentTime <= 60f) // Kurang dari 1 menit (3 Bintang)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(true);
        }
        else if (currentTime <= 90f) // Kurang dari 1m 30s (2 Bintang)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
        }
        else if (currentTime <= 120f) // Kurang dari 2 menit (1 Bintang)
        {
            stars[0].SetActive(true);
        }
        // Jika lebih dari 2 menit, 0 bintang (tidak ada yang aktif)
    }
}