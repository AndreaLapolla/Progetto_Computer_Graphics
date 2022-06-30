using UnityEngine;

// classe per gestire i suoni del gioco
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private AudioSource _audioSource;

    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }
}
