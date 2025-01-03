using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public record AudioCue
{
    public string Name;
    public List<AudioSource> AudioSources;
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private List<AudioCue> _audioCues;

    public void PlayCue(string cueName)
    {
        _audioCues.Find(cue => cue.Name == cueName)?.AudioSources.GetRandom().Play();
    }
}