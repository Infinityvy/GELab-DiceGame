using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider masterVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;

    public static float musicVolume { get; private set; }
    public static float sfxVolume { get; private set; }

    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;

    private void Start() {
        if (!PlayerPrefs.HasKey("masterVolume"))
            PlayerPrefs.SetFloat("masterVolume", 1);
        if (!PlayerPrefs.HasKey("musicVolume"))
            PlayerPrefs.SetFloat("musicVolume", 1);
        if (!PlayerPrefs.HasKey("sfxVolume"))
            PlayerPrefs.SetFloat("sfxVolume", 1);
        Load();

        for (int i = 0; i < SoundAssets.MusicSources.Count; i++) {
            SoundAssets.MusicSources[i].outputAudioMixerGroup = musicMixerGroup;
        }

        for (int i = 0; i < SoundAssets.SFXSources.Count; i++) {
            SoundAssets.SFXSources[i].outputAudioMixerGroup = sfxMixerGroup;
        }
    }

    public void ChangeMasterVolume(float value) {
        AudioListener.volume = value;
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
    /// Loads the values of the volume sliders into the sliders.
    /// </summary>
    public void Load() {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    /// <summary>
    /// Saves the values of the volume sliders.
    /// </summary>
    public void Save() {
        PlayerPrefs.SetFloat("masterVolume", masterVolumeSlider.value);
        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolumeSlider.value);
    }

    private void OnApplicationQuit() {
        PlayerPrefs.Save();
    }
}