using UnityEngine;
using UnityEngine.UI; // Gunakan TMPro jika kamu pakai TextMeshPro
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void GoToLevelSelection()
    {
        // Pastikan nama scene sesuai dengan yang ada di Build Settings
        SceneManager.LoadScene("level selection");
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