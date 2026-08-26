using UnityEngine;
using UnityEngine.UI; // Gunakan TMPro jika kamu pakai TextMeshPro
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void GoToMainMenu()
    {
        // Pastikan nama scene sesuai dengan yang ada di Build Settings
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToLevelSelection()
    {
        // Pastikan nama scene sesuai dengan yang ada di Build Settings
        SceneManager.LoadScene("level selection");
    }

    public void GoToLevel1()
    {
        // Pastikan nama scene sesuai dengan yang ada di Build Settings
        SceneManager.LoadScene("level 1");
    }
    public void GoToLevel2()
    {
        // Pastikan nama scene sesuai dengan yang ada di Build Settings
        SceneManager.LoadScene("level 2");
    }
    public void GoToLevel3()
    {
        // Pastikan nama scene sesuai dengan yang ada di Build Settings
        SceneManager.LoadScene("level 3");
    }

    public void GoToInformation()
    {
        // Pastikan nama scene sesuai dengan yang ada di Build Settings
        SceneManager.LoadScene("Information");
    }

    // Pasang di tombol "GOT IT" pada Popup_Information
    public void GoToCredit()
    {
        // Pastikan nama scene sesuai dengan yang ada di Build Settings
        SceneManager.LoadScene("Credit");
    }
}