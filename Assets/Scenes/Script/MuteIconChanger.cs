using UnityEngine;
using UnityEngine.UI;

public class MuteToggleUI : MonoBehaviour
{
    public Image iconImage;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private void Start()
    {
        UpdateIcon();
    }

    public void TekanTombolMute()
    {
        AudioManager.instance.ToggleMute();
        UpdateIcon();
    }

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