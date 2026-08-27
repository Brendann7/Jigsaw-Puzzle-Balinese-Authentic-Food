using UnityEngine;

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

    private bool isMuted = false;

    private void Awake()
    {
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

    public void PlaySpecificSound(AudioClip clip)
    {
        if (clip != null) sfxSource.PlayOneShot(clip);
    }
}