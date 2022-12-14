using UnityEngine.UIElements;
using UnityEngine;

public class VideoSettingsVE : VisualElement
{

    // Initialising the Canvas variables;
    private DropdownField m_resolutionDropdown;
    private Toggle m_fullscreenToggle;
    private Slider m_brightnessSlider;
    private DropdownField m_refreshRateDropdown;
    private Toggle m_vsyncToggle;

    // Private Constants for Values
    private const string k_Brightness = "Brightness";


    // Canvas referral
    private bool m_isFullscreen;
    private bool m_isVsync;
    // private float m_brightnessValue;
    private int m_CurrentResolutionIndex;

    // Main
    public new class UxmlFactory:UxmlFactory<VideoSettingsVE, UxmlTraits>{}
    public VideoSettingsVE()
    {
        VisualTreeAsset visualTree = Resources.Load<VisualTreeAsset>("UI/MenuMain/VideoSettings");
        visualTree.CloneTree(this);

        m_resolutionDropdown = this.Q<DropdownField>("resolution-dropdown");
        m_refreshRateDropdown = this.Q<DropdownField>("refresh-rate-dropdown");
        m_brightnessSlider = this.Q<Slider>("brightness-slider");
        m_vsyncToggle = this.Q<Toggle>("vsync-toggle");
        m_fullscreenToggle = this.Q<Toggle>("fullscreen-toggle");

        m_brightnessSlider.value = PlayerPrefs.HasKey(k_Brightness) ? PlayerPrefs.GetFloat(k_Brightness) : 1f;

        //
        m_isFullscreen = Screen.fullScreen;
        // m_brightnessValue = Screen.brightness;
        // Debug.Log(m_brightnessValue);
    }


    // Functions
    #region Resolution
    private void OnResolutionChanged()
    {
        Resolution resolution = Screen.resolutions[m_CurrentResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, m_isFullscreen);
        SetResolutionField();
    }

    private void SetResolutionField()
    {
        string displayText = Screen.resolutions[m_CurrentResolutionIndex].ToString();
        displayText = displayText.Substring(0, displayText.IndexOf("@"));
        // m_ResolutionLabel.text = displayText;
    }

    // functions run on next and previous res click
    private void PreviousResolution()
    {
        m_CurrentResolutionIndex = Mathf.Clamp(m_CurrentResolutionIndex - 1, 0, Screen.resolutions.Length - 1);
        OnResolutionChanged();
    }

    private void NextResolution()
    {
        m_CurrentResolutionIndex = Mathf.Clamp(m_CurrentResolutionIndex + 1, 0, Screen.resolutions.Length - 1);
        OnResolutionChanged();
    }
    #endregion

    #region Fullscreen
    private void FullscreenOff()
    {
        m_isFullscreen = false;
        OnFullscreenChanged();
    }

    private void FullscreenOn()
    {
        m_isFullscreen = true;
        OnFullscreenChanged();
    }

    private void OnFullscreenChanged()
    {
        Screen.fullScreen = m_isFullscreen;
        SetFullScreenToggle();
    }
    
    private void SetFullScreenToggle()
    {
        m_fullscreenToggle.value = Screen.fullScreen;
    }
    #endregion


    #region MenuButtons -- accessed in SettingsVE
    // public void SaveSettings()
    // {
    //     PlayerPrefs.SetInt(k_ResolutionIndex, m_CurrentResolutionIndex);
    //     // Playerprefs does not understand booleans so converted to integer - false val is 0, true is 1
    //     PlayerPrefs.SetInt(k_Fullscreen, Convert.ToInt32(m_isFullscreen));
    //     PlayerPrefs.SetFloat(k_Gamma, m_gammaSlider.value);
    // }

    // public void ResetSettings()
    // {
    //     m_CurrentResolutionIndex = PlayerPrefs.GetInt(k_ResolutionIndex);
    //     OnResolutionChanged();
    //     // Converted back to Bool from 0 or 1 int or error
    //     m_isFullscreen = Convert.ToBoolean(PlayerPrefs.GetInt(k_Fullscreen));
    //     OnFSChanged();
    //     m_gammaSlider.value = PlayerPrefs.GetFloat(k_Gamma);
    // }
    #endregion
}
