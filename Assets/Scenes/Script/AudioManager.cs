using UnityEngine;

// Central audio singleton that plays background music and sound effects
// for the whole game.
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public AudioClip buttonSound;
    public AudioClip dragPuzzleSound;
    public AudioClip dropPuzzleSound;
    public AudioClip finishGameSound;
    public AudioClip popUpSound;
    public AudioClip pronounceFoodSound;

    public bool isMuted = false;

    private void Awake()
    {
        // Standard singleton pattern: keep the first instance alive across scenes,
        // destroy any duplicates
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Toggles global audio mute on/off
    public void ToggleMute()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0f : 1f;
    }

    public void PlayButton()
    {
        if (buttonSound != null) sfxSource.PlayOneShot(buttonSound);
    }

    public void PlayDragPuzzle()
    {
        if (dragPuzzleSound != null) sfxSource.PlayOneShot(dragPuzzleSound);
    }

    public void PlayDropPuzzle()
    {
        if (dropPuzzleSound != null) sfxSource.PlayOneShot(dropPuzzleSound);
    }

    public void PlayFinishGame()
    {
        if (finishGameSound != null) sfxSource.PlayOneShot(finishGameSound);
    }

    public void PlayPopUp()
    {
        if (popUpSound != null) sfxSource.PlayOneShot(popUpSound);
    }

    // Plays an arbitrary clip passed in by the caller
    public void PlaySpecificSound(AudioClip clip)
    {
        if (clip != null) sfxSource.PlayOneShot(clip);
    }
}