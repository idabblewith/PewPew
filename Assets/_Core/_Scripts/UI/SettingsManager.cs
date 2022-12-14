using System;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{

    private const string k_ResolutionIndex = "ResolutionIndex";
    private const string k_Fullscreen = "Fullscren";

    private void Awake() 
    {
        Resolution currentResolution = Screen.resolutions[PlayerPrefs.GetInt(k_ResolutionIndex)];
        Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt(k_Fullscreen));
        Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
    }
}
