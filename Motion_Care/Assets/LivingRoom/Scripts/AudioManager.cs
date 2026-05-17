using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public void ReproducirAudio(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    public bool EstaReproduciendo()
    {
        return audioSource.isPlaying;
    }
}
