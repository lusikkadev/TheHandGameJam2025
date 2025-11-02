using System.Collections;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private float delay = 3f;
    private void Awake()
    {
        StartCoroutine(PlayDelayed());
    }
    private IEnumerator PlayDelayed() 
    {
        yield return new WaitForSeconds(delay);

        source.Play();
    }
}
