using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.Audio;

/*
    Handler for audio with reference to Audiomixer and AudioSetting Visual Element in Resources folder.
    Purpose: Enables adjusting of audio (Works! :D)

    Updated - 6/12/22 - Jarid Prince 
*/

public class AudioSettingsVE : VisualElement
{
    // Initialising the slider variables
    private Slider m_MasterVolumeSlider;
    private Slider m_MusicVolumeSlider;
    private Slider m_SFXVolumeSlider;
    private Slider m_MyVoiceVolumeSlider;
    private Slider m_OtherVoiceVolumeSlider;

    // Private Constants for volume strings
    private const string k_MasterVolume = "MasterVolume";
    private const string k_MusicVolume = "MusicVolume";
    private const string k_SFXVolume = "SFXVolume";
    private const string k_MyVoiceVolume = "MyVoiceVolume";
    private const string k_OtherVoiceVolume = "OtherVoiceVolume";

  // UXML Factory allows the code to show up as a custom control in UIBuilder
    public new class UxmlFactory:UxmlFactory<AudioSettingsVE, UxmlTraits> {}
    
    // // Constructor
    public AudioSettingsVE()
    {
        // Reference to UXML file (via Resources relative path) for audio settings
        VisualTreeAsset visualTree = Resources.Load<VisualTreeAsset>("UI/MenuMain/AudioSettings");
        visualTree.CloneTree(this);

        // Querying & assigning sliders in cloned document
        m_MasterVolumeSlider = this.Q<Slider>("master-volume-slider");
        m_MusicVolumeSlider = this.Q<Slider>("music-volume-slider");
        m_SFXVolumeSlider = this.Q<Slider>("sfx-volume-slider");
        m_MyVoiceVolumeSlider = this.Q<Slider>("myvoice-volume-slider");
        m_OtherVoiceVolumeSlider = this.Q<Slider>("othervoice-volume-slider");

        // Sets value of slider based on whether a key exists already (so set each time run to last set value), otherwise max
        // See also AudioManager.cs
        m_MasterVolumeSlider.value = PlayerPrefs.HasKey(k_MasterVolume) ? PlayerPrefs.GetFloat(k_MasterVolume) : 1f;
        m_MusicVolumeSlider.value = PlayerPrefs.HasKey(k_MusicVolume) ? PlayerPrefs.GetFloat(k_MusicVolume) : 1f;
        m_SFXVolumeSlider.value = PlayerPrefs.HasKey(k_SFXVolume) ? PlayerPrefs.GetFloat(k_SFXVolume) : 1f;
        m_MyVoiceVolumeSlider.value = PlayerPrefs.HasKey(k_MyVoiceVolume) ? PlayerPrefs.GetFloat(k_MyVoiceVolume) : 1f;
        m_OtherVoiceVolumeSlider.value = PlayerPrefs.HasKey(k_OtherVoiceVolume) ? PlayerPrefs.GetFloat(k_OtherVoiceVolume) : 1f;

        // Reference to audiomixer in Resources to control via code
        AudioMixer audioMixer = Resources.Load<AudioMixer>("Audio/Main");
    
        // Master
        m_MasterVolumeSlider.RegisterValueChangedCallback(evt =>
        {
            audioMixer.SetFloat(k_MasterVolume, Mathf.Log10(evt.newValue) * 20);
        });
        // Music
        m_MusicVolumeSlider.RegisterValueChangedCallback(evt =>
        {
            audioMixer.SetFloat(k_MusicVolume, Mathf.Log10(evt.newValue) * 20);
        });
        // SFX
        m_SFXVolumeSlider.RegisterValueChangedCallback(evt =>
        {
            audioMixer.SetFloat(k_SFXVolume, Mathf.Log10(evt.newValue) * 20);
        });
        // My Voice
        m_MyVoiceVolumeSlider.RegisterValueChangedCallback(evt =>
        {
            audioMixer.SetFloat(k_MyVoiceVolume, Mathf.Log10(evt.newValue) * 20);
        });
        // Other Voice
        m_OtherVoiceVolumeSlider.RegisterValueChangedCallback(evt =>
        {
            audioMixer.SetFloat(k_OtherVoiceVolume, Mathf.Log10(evt.newValue) * 20);
        });
    }

    // // Saving Values with PlayerPrefs which is like React/JS Wdev localstorage key/value
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(k_MasterVolume, m_MasterVolumeSlider.value);
        PlayerPrefs.SetFloat(k_MusicVolume, m_MusicVolumeSlider.value);
        PlayerPrefs.SetFloat(k_SFXVolume, m_SFXVolumeSlider.value);
        PlayerPrefs.SetFloat(k_MyVoiceVolume, m_MyVoiceVolumeSlider.value);
        PlayerPrefs.SetFloat(k_OtherVoiceVolume, m_OtherVoiceVolumeSlider.value);
    }

    // // Resets sliders by injecting previously saved values
    public void ResetSettings()
    {
        m_MasterVolumeSlider.value = PlayerPrefs.GetFloat(k_MasterVolume);
        m_MusicVolumeSlider.value = PlayerPrefs.GetFloat(k_MusicVolume);
        m_SFXVolumeSlider.value = PlayerPrefs.GetFloat(k_SFXVolume);
        m_MyVoiceVolumeSlider.value = PlayerPrefs.GetFloat(k_MyVoiceVolume);
        m_OtherVoiceVolumeSlider.value = PlayerPrefs.GetFloat(k_OtherVoiceVolume);
    }
}
