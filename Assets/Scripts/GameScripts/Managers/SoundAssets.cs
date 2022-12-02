using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundAssets
{
    // Lists for music and sfx sources - is filled and managed by SoundManager
    public static List<AudioSource> MusicSources = new();
    public static List<AudioSource> SFXSources = new();

    public static void AddMusic(AudioSource audioSource) {
        if(!MusicSources.Contains(audioSource) && audioSource != null) MusicSources.Add(audioSource);
    }

    public static void AddSFX(AudioSource audioSource) {
        if (!SFXSources.Contains(audioSource) && audioSource != null) SFXSources.Add(audioSource);
    }
}