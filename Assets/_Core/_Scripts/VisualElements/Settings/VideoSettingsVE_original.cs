using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/*
    Handler for video resolution and gamma

    Updated - 6/12/22 - Jarid Prince 
*/

public class VideoSettingsVE_original : VisualElement
{
    // UXML Factory allows the code to show up as a custom control in UIBuilder
    public new class UxmlFactory : UxmlFactory<VideoSettingsVE_original, UxmlTraits> { }

    private Label m_ResolutionLabel;
    private Label m_FullscreenLabel;

    // Slider
    private Slider m_gammaSlider;

    private const string k_ResolutionIndex = "ResolutionIndex";
    private const string k_Fullscreen = "Fullscren";
    private const string k_Gamma = "Gamna";

    private int m_CurrentResolutionIndex;
    private bool m_isFullscreen;
    private float m_Gamma;
    // private List<Resolution> m_Resolutions = new List<Resolution>();

    // Constructor 
    public VideoSettingsVE_original()
    {
        // Reference to UXML file (via Resources relative path) for video settings
        VisualTreeAsset visualTree = Resources.Load<VisualTreeAsset>("UI/videoSettings");
        visualTree.CloneTree(this); // cloned so as to not accidentally edit

        // Populate var
        m_ResolutionLabel = this.Q<Label>("resolution-label");
        m_FullscreenLabel = this.Q<Label>("fullscreen-label");
        DropdownField resolutionDropdown = this.Q<DropdownField>("resolution-dropdown");
        m_gammaSlider = this.Q<Slider>("gamma-slider");


        // TODO clicked functions next and previous reso
        // ....

        Button nextResBtn = this.Q<Button>("next-res-button");
        Button prevResBtn = this.Q<Button>("prev-res-button");
        nextResBtn.clicked += NextResolution;
        prevResBtn.clicked += PreviousResolution;

        Button nextFullscreenBtn = this.Q<Button>("next-fs-button");
        Button prevFullscreenBtn = this.Q<Button>("prev-fs-button");
        nextFullscreenBtn.clicked += NextFS;
        prevFullscreenBtn.clicked += PreviousFS;
        /// ......


        // foreach (Resolution res in Screen.resolutions)
        // {
        //     m_Resolutions.Add(res);
        // }

        m_isFullscreen = Screen.fullScreen;
        m_Gamma = Screen.brightness;
        SetFSField();

        if (PlayerPrefs.HasKey(k_ResolutionIndex)) // if prev settings, set to those
        {
            m_CurrentResolutionIndex = PlayerPrefs.GetInt(k_ResolutionIndex);

        }
        else // otherwise, set to current screen res
        {
            m_CurrentResolutionIndex = Array.FindIndex(Screen.resolutions, resolution =>
                resolution.width == Screen.currentResolution.width && resolution.height == Screen.currentResolution.height);
        }
        OnResolutionChanged();

        m_gammaSlider.value = PlayerPrefs.HasKey(k_Gamma) ? PlayerPrefs.GetFloat(k_Gamma) : 1f;
        m_gammaSlider.RegisterValueChangedCallback(e =>
        {
            Screen.brightness = m_gammaSlider.value;
        });
    }

    // functions run on next and prev fs click
    private void PreviousFS()
    {
        m_isFullscreen = false;
        OnFSChanged();
    }

    private void NextFS()
    {
        m_isFullscreen = true;
        OnFSChanged();
    }

    private void OnFSChanged()
    {
        Screen.fullScreen = m_isFullscreen;
        SetFSField();
    }

    private void SetFSField()
    {
        m_FullscreenLabel.text = m_isFullscreen ? "On" : "Off";
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
        m_ResolutionLabel.text = displayText;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt(k_ResolutionIndex, m_CurrentResolutionIndex);
        // Playerprefs does not understand booleans so converted to integer - false val is 0, true is 1
        PlayerPrefs.SetInt(k_Fullscreen, Convert.ToInt32(m_isFullscreen));
        PlayerPrefs.SetFloat(k_Gamma, m_gammaSlider.value);
    }

    public void ResetSettings()
    {
        m_CurrentResolutionIndex = PlayerPrefs.GetInt(k_ResolutionIndex);
        OnResolutionChanged();
        // Converted back to Bool from 0 or 1 int or error
        m_isFullscreen = Convert.ToBoolean(PlayerPrefs.GetInt(k_Fullscreen));
        OnFSChanged();
        m_gammaSlider.value = PlayerPrefs.GetFloat(k_Gamma);
    }

}
