using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MinigameAudioManager : Singleton<MinigameAudioManager>
{
    // VARIABLES //
    
    [Header("Source")]
    [SerializeField] private AudioSource source;
    
    [Header("Clips")]
    [SerializeField] private List<AudioClip> clips;
    
    private Dictionary<string, AudioClip> _stringToClip;
    
    
    // METHODS && FUNCTIONS //

    private void Awake()
    {
        source ??= GetComponent<AudioSource>();
        
        _stringToClip = new Dictionary<string, AudioClip>();

        foreach (AudioClip clip in clips)
        {
            _stringToClip[clip.name] = clip;
        }
    }

    public void PlayClip(string clipName)
    {
        source.PlayOneShot(_stringToClip[clipName]);
    }

    public void StopAudio()
    {
        source.Stop();
    }
}
