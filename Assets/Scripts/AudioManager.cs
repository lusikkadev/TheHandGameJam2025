using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic;

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
}
