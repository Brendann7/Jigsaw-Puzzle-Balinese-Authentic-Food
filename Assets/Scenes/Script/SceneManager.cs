using UnityEngine;
using UnityEngine.UI; // Use TMPro if you're using TextMeshPro
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void GoToMainMenu()
    {
        // Make sure the scene name matches the one in Build Settings
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToLevelSelection()
    {
        // Make sure the scene name matches the one in Build Settings
        SceneManager.LoadScene("level selection");
    }

    public void GoToLevel1()
    {
        // Make sure the scene name matches the one in Build Settings
        SceneManager.LoadScene("level 1");
    }
    public void GoToLevel2()
    {
        // Make sure the scene name matches the one in Build Settings
        SceneManager.LoadScene("level 2");
    }
    public void GoToLevel3()
    {
        // Make sure the scene name matches the one in Build Settings
        SceneManager.LoadScene("level 3");
    }

    public void GoToInformation()
    {
        // Make sure the scene name matches the one in Build Settings
        SceneManager.LoadScene("Information");
    }

    // Attach this to the "GOT IT" button on Popup_Information
    public void GoToCredit()
    {
        // Make sure the scene name matches the one in Build Settings
        SceneManager.LoadScene("Credit");
    }

    public void Quit()
    {
        // Quit the application when built (PC/Android/etc.)
        Application.Quit();

        // Stop Play Mode when testing inside the Unity Editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}