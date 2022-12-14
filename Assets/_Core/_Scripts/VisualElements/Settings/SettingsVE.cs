using UnityEngine;
// using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SettingsVE : VisualElement
{
    private PlainButton m_VideoButton;
    private PlainButton m_AudioButton;
    private PlainButton m_MiscButton;
    private AudioSettingsVE m_AudioSettings;
    private VideoSettingsVE m_VideoSettings;
    // private MiscSettingsVE m_MiscSettings;

    // // UXML Factory allows the code to show up as a custom control in UIBuilder
    public new class UxmlFactory:UxmlFactory<SettingsVE, UxmlTraits> {}

    // // Constructor
    public SettingsVE()
    {
        // Reference to UXML file (via Resources relative path) for main settings
        VisualTreeAsset visualTree = Resources.Load<VisualTreeAsset>("UI/MenuMain/Settings");
        visualTree.CloneTree(this); // cloned so as to not accidentally edit

        m_VideoButton = this.Q<PlainButton>("video-button");
        m_AudioButton = this.Q<PlainButton>("audio-button");
        m_MiscButton = this.Q<PlainButton>("misc-button");

        m_AudioSettings = this.Q<AudioSettingsVE>();
        m_VideoSettings = this.Q<VideoSettingsVE>();
        // m_MiscSettings = this.Q<MiscSettingsVE>();

        m_VideoButton.clicked += ShowVideoSettings;
        m_AudioButton.clicked += ShowAudioSettings;
        m_MiscButton.clicked += ShowMiscSettings;
        // References to save and reset buttons on VE tree, runs functions when clicked
        Button saveBtn = this.Q<Button>("save-button");
        saveBtn.clicked += SaveSettings;

        Button resetBtn = this.Q<Button>("reset-button");
        resetBtn.clicked += ResetSettings;
    }

     private void HideAllOptions()
    {
        HideVideoSettings();
        HideAudioSettings();
        HideMiscSettings();
    }

    private void ShowVideoSettings()
    {
        HideAllOptions();
        m_VideoSettings.style.display = DisplayStyle.Flex;
    }

    private void ShowAudioSettings()
    {
        HideAllOptions();
        m_AudioSettings.style.display = DisplayStyle.Flex;

    }

    private void ShowMiscSettings()
    {
        HideAllOptions();
        m_AudioSettings.style.display = DisplayStyle.Flex;
    }

    private void HideVideoSettings()
    {
        m_VideoSettings.style.display = DisplayStyle.None;
    }

    private void HideAudioSettings()
    {
        m_AudioSettings.style.display = DisplayStyle.None;

    }

    private void HideMiscSettings()
    {
        m_VideoSettings.style.display = DisplayStyle.None;
    }

    private void SaveSettings()
    {
        m_AudioSettings.SaveSettings();
        // m_VideoSettings.SaveSettings();
        Debug.Log("Save clicked");
    }

    private void ResetSettings()
    {
        m_AudioSettings.ResetSettings();
        // m_VideoSettings.ResetSettings();
        Debug.Log("Reset clicked");
    }
}
