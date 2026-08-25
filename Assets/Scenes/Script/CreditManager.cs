using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour
{
    // Fungsi ini akan dipanggil saat tombol Back ditekan
    public void GoBackToMenu()
    {
        // Ganti "MainMenu" dengan nama scene tujuan kalian yang sebenarnya
        SceneManager.LoadScene("MainMenu"); 
    }
}