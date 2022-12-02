using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider masterVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;

    private void Start() {
        Debug.Log(SoundAssets.SFXSources[0]);
        if (!PlayerPrefs.HasKey("masterVolume"))
            PlayerPrefs.SetFloat("masterVolume", 1);
        if (!PlayerPrefs.HasKey("musicVolume"))
            PlayerPrefs.SetFloat("musicVolume", 1);
        if (!PlayerPrefs.HasKey("sfxVolume"))
            PlayerPrefs.SetFloat("sfxVolume", 1);
        Load();
    }

    public void ChangeMasterVolume() {
        AudioListener.volume = masterVolumeSlider.value;
        Save();
    }

    public void ChangeMusicVolume() {
        for (int i = 0; i < SoundAssets.MusicSources.Count; i++) {
            SoundAssets.MusicSources[i].volume = musicVolumeSlider.value;
        }
        Save();
    }

    public void ChangeSFXVolume() {
        for (int i = 0; i < SoundAssets.SFXSources.Count; i++) {
            SoundAssets.SFXSources[i].volume = sfxVolumeSlider.value;
        }
        Save();
    }

    public void Load() {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    public void Save() {
        PlayerPrefs.SetFloat("masterVolume", masterVolumeSlider.value);
        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolumeSlider.value);
    }
}