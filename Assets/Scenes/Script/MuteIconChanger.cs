using UnityEngine;
using UnityEngine.UI;

// Updates the mute button icon to reflect the current mute state
public class MuteToggleUI : MonoBehaviour
{
    public Image iconImage;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private void Start()
    {
        UpdateIcon();
    }

    // Hooked up to the mute button's OnClick event
    // (name kept as-is to avoid breaking existing Inspector/Button wiring)
    public void PressMuteButton()
    {
        AudioManager.instance.ToggleMute();
        UpdateIcon();
    }

    // Swaps the icon sprite based on the current mute state
    private void UpdateIcon()
    {
        if (AudioManager.instance.isMuted)
        {
            iconImage.sprite = soundOffSprite;
        }
        else
        {
            iconImage.sprite = soundOnSprite;
        }
    }
}