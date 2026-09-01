using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour
{
    // Called when the Back button is pressed
    public void GoBackToMenu()
    {
        // Replace "MainMenu" with your actual target scene name
        SceneManager.LoadScene("MainMenu"); 
    }
}