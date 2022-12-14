using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer m_AudioMixer;

    private const string k_MasterVolume = "MasterVolume";
    private const string k_MusicVolume = "MusicVolume";
    private const string k_SFXVolume = "SFXVolume";
    private const string k_MyVoiceVolume = "MyVoiceVolume";
    private const string k_OtherVoiceVolume = "OtherVoiceVolume";

    private void Awake() 
    {
        m_AudioMixer.SetFloat(k_MasterVolume, Mathf.Log10(PlayerPrefs.HasKey(k_MasterVolume) ? PlayerPrefs.GetFloat(k_MasterVolume) : 1f) * 20);
        m_AudioMixer.SetFloat(k_MusicVolume, Mathf.Log10(PlayerPrefs.HasKey(k_MusicVolume) ? PlayerPrefs.GetFloat(k_MusicVolume) : 1f) * 20);
        m_AudioMixer.SetFloat(k_SFXVolume, Mathf.Log10(PlayerPrefs.HasKey(k_SFXVolume) ? PlayerPrefs.GetFloat(k_SFXVolume) : 1f) * 20);
        m_AudioMixer.SetFloat(k_MyVoiceVolume, Mathf.Log10(PlayerPrefs.HasKey(k_MyVoiceVolume) ? PlayerPrefs.GetFloat(k_MyVoiceVolume) : 1f) * 20);
        m_AudioMixer.SetFloat(k_OtherVoiceVolume, Mathf.Log10(PlayerPrefs.HasKey(k_OtherVoiceVolume) ? PlayerPrefs.GetFloat(k_OtherVoiceVolume) : 1f) * 20);
    }
}
