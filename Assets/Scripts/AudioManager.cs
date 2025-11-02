using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public AudioClip walkingSFX;
    public AudioClip crows;
    public AudioClip rumble;
    public AudioClip jump;
    public AudioClip land;

    private AudioSource audioSource;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (audioSource != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void PlayCrowSound()
    {
        PlaySound(crows);
    }
    public void PlayLandSound()
    {
        PlaySound(land);
    }

    public void PlayJumpSound()
    {
        PlaySound(jump);
    }

    public void PlayRumbleSound()
    {
        PlaySound(rumble);
    }
}
