using UnityEngine;

// Helper component meant to be hooked up to UI Button OnClick events,
// forwarding calls to the AudioManager singleton.
public class AudioCall : MonoBehaviour
{
    // Plays the generic button click sound
    public void GeneralAudio()
    {
        AudioManager.instance.PlayButton();
    }

    // Plays a specific audio clip passed in from the Inspector
    public void SpecificAudio(AudioClip clip)
    {
        AudioManager.instance.PlaySpecificSound(clip);
    }

    // Toggles the global mute state
    public void ClickMute()
    {
        AudioManager.instance.ToggleMute();
    }

    // Plays the popup open sound
    public void PopUp()
    {
      AudioManager.instance.PlayPopUp();
    }
}