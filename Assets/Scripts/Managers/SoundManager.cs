using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider masterVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;

    public static float masterVolume { get; private set; }
    public static float musicVolume { get; private set; }
    public static float sfxVolume { get; private set; }

    public AudioMixerGroup musicMixerGroup;
    public AudioMixerGroup sfxMixerGroup;

    private void Start() {
        if (!PlayerPrefs.HasKey("masterVolume"))
            PlayerPrefs.SetFloat("masterVolume", 1);
        if (!PlayerPrefs.HasKey("musicVolume"))
            PlayerPrefs.SetFloat("musicVolume", 1);
        if (!PlayerPrefs.HasKey("sfxVolume"))
            PlayerPrefs.SetFloat("sfxVolume", 1);
        Load();

        //for (int i = 0; i < SoundAssets.MusicSources.Count; i++) {
        //    if (SoundAssets.MusicSources[i]) {
        //        SoundAssets.MusicSources[i].outputAudioMixerGroup = musicMixerGroup;
        //    }
        //}

        //for (int i = 0; i < SoundAssets.SFXSources.Count; i++) {
        //    if (SoundAssets.SFXSources[i]) {
        //        SoundAssets.SFXSources[i].outputAudioMixerGroup = sfxMixerGroup;
        //    }
        //}
    }

    public void OnMasterVolumeSliderChange(float value) {
        masterVolume = value;
        AudioListener.volume = masterVolume;
        Save();
    }

    public void OnMusicVolumeSliderChange(float value) {
        musicVolume = value;
        UpdateMixerVolume();
        Save();
    }

    public void OnSFXVolumeSliderChange(float value) {
        sfxVolume = value;
        UpdateMixerVolume();
        Save();
    }

    public void UpdateMixerVolume() {   // convert to decibel with mathf
        musicMixerGroup.audioMixer.SetFloat("Music Volume", Mathf.Log10(musicVolume) * 20);
        sfxMixerGroup.audioMixer.SetFloat("SFX Volume", Mathf.Log10(sfxVolume) * 20);
    }

    /// <summary>
    /// Loads volume values saved from a previous session.
    /// </summary>
    public void Load() {
        masterVolume = PlayerPrefs.GetFloat("masterVolume");
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        sfxVolume = PlayerPrefs.GetFloat("sfxVolume");

        masterVolumeSlider.value = masterVolume;
        musicVolumeSlider.value = musicVolume;
        sfxVolumeSlider.value = sfxVolume;

        AudioListener.volume = masterVolume;
        UpdateMixerVolume();
    }

    /// <summary>
    /// Saves the values of the set volume values.
    /// </summary>
    public void Save() {
        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
    }

    private void OnApplicationQuit() {
        PlayerPrefs.Save();
    }
}