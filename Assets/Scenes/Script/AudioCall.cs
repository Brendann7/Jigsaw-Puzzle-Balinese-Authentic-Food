using UnityEngine;

public class AudioCall : MonoBehaviour
{
    public void GeneralAudio()
    {
        AudioManager.instance.PlayButton();
    }

    public void SpecificAudio(AudioClip clip)
    {
        AudioManager.instance.PlaySpecificSound(clip);
    }

    public void ClickMute()
    {
        AudioManager.instance.ToggleMute();
    }

    public void PopUp()
    {
      AudioManager.instance.PlayPopUp();
    }
}