using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MinigameAudioManager : Singleton<MinigameAudioManager>
{
    // VARIABLES //
    
    [Header("Sources")]
    [SerializeField] private AudioSource mainSource;
    [SerializeField] private List<AudioSource> spatialAudioSources;
    
    [Header("Clips")]
    [SerializeField] private List<AudioClip> clips;
    
    private Dictionary<string, AudioClip> _stringToClip;
    
    
    // METHODS && FUNCTIONS //

    private void Awake()
    {
        mainSource ??= GetComponent<AudioSource>();
        
        _stringToClip = new Dictionary<string, AudioClip>();

        foreach (AudioClip clip in clips)
        {
            _stringToClip[clip.name] = clip;
        }
    }

    public void PlayClip(string clipName)
    {
        mainSource.PlayOneShot(_stringToClip[clipName]);
    }

    public void StopAudio()
    {
        mainSource.Stop();
    }
    
    public void PlaySpatialClip(string clipName, int spatialSourceIndex)
    {
        spatialAudioSources[spatialSourceIndex].PlayOneShot(_stringToClip[clipName]);
    }
    
    public float GetClipLength(string clipName)
    {
        return _stringToClip[clipName].length;
    }
}
