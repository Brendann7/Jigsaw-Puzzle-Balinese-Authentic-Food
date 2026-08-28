using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Popups")]
    public GameObject popupInstruction;
    public GameObject popupScore;
    public GameObject popupInformation;
    public GameObject popupGameOver; 

    [Header("Game UI & Timer")]
    public Text timerText; 
    public Text finalTimeText; 
    public Text feedbackText; 

    [Header("Puzzle Settings")]
    public int totalPieces = 10;
    private int placedPieces = 0;

    [Header("Score Stars (Bintang)")]
    public GameObject[] stars;

    [Header("Game Rules & Star Thresholds")]
    public float timeLimit = 90f; 
    public float threeStarLimit = 40f; 
    public float twoStarLimit = 65f; 

    private float currentTime = 0f;
    private bool isTimerRunning = false;
    private bool isGameOver = false;

    void Start()
    {
        popupInstruction.SetActive(true);
        popupScore.SetActive(false);
        popupInformation.SetActive(false);
        if (popupGameOver != null) popupGameOver.SetActive(false);

        isTimerRunning = false;
        currentTime = 0f;
        isGameOver = false;
    }

    void Update()
    {
        if (isTimerRunning && !isGameOver)
        {
            currentTime += Time.deltaTime;
            UpdateTimerUI();

            if (currentTime >= timeLimit)
            {
                TriggerGameOver();
            }
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartGame()
    {
        popupInstruction.SetActive(false);
        isTimerRunning = true;
    }

    public void ShowInformation()
    {
        popupScore.SetActive(false);
        popupInformation.SetActive(true);
    }

    public void AddPlacedPiece()
    {
        if (isGameOver) return; 

        placedPieces++;

        if (placedPieces >= totalPieces)
        {
            FinishGame();
        }
    }

    void FinishGame()
    {
        isTimerRunning = false;
        isGameOver = true;
        AudioManager.instance.PlayFinishGame();
        popupScore.SetActive(true);

        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        if (finalTimeText != null) finalTimeText.text = "USER TIME : " + string.Format("{0:00}:{1:00}", minutes, seconds);

        CalculateStars();
    }

    void TriggerGameOver()
    {
        isTimerRunning = false;
        isGameOver = true;
        if (popupGameOver != null) popupGameOver.SetActive(true);
    }

    void CalculateStars()
    {
        foreach (GameObject star in stars)
        {
            if (star != null) star.SetActive(false);
        }

        if (currentTime <= threeStarLimit) 
        {
            if (stars.Length > 0) stars[0].SetActive(true);
            if (stars.Length > 1) stars[1].SetActive(true);
            if (stars.Length > 2) stars[2].SetActive(true);
            if (feedbackText != null) feedbackText.text = "EXCELLENT!";
        }
        else if (currentTime <= twoStarLimit) 
        {
            if (stars.Length > 0) stars[0].SetActive(true);
            if (stars.Length > 1) stars[1].SetActive(true);
            if (feedbackText != null) feedbackText.text = "GREAT JOB!";
        }
        else 
        {
            if (stars.Length > 0) stars[0].SetActive(true);
            if (feedbackText != null) feedbackText.text = "GOOD TRY!";
        }
    }
}