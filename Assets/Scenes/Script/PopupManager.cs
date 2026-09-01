using UnityEngine;

// Generic helper for opening/closing any popup panel passed in from the Inspector or an event
public class PopUpManager : MonoBehaviour
{
    // Activates the given panel
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    // Deactivates the given panel
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}