using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.Audio;

/*
    Handler for audio with reference to Audiomixer and AudioSetting Visual Element in Resources folder.
    Purpose: Enables adjusting of audio (Works! :D)

    Updated - 6/12/22 - Jarid Prince 
*/

public class SaveFileVE : VisualElement
{
    // // UXML Factory allows the code to show up as a custom control in UIBuilder
    // public new class UxmlFactory:UxmlFactory<SaveFileVE, UxmlTraits> {}

    // // Initialising the slider variables
    // private Button m_PlayBtn;
    // private Button m_CopyBtn;
    // private Button m_DeleteBtn;

    // // Private Constants for Player Files
    // private const string k_PlayernName = "PlayerName";
    // private const string k_PlayTime = "PlayTime";

    // // Constructor
    // public SaveFileVE()
    // {
    //     // Reference to UXML file (via Resources relative path) for save settings
    //     VisualTreeAsset visualTree = Resources.Load<VisualTreeAsset>("UI/savefile");
    //     visualTree.CloneTree(this); // cloned so as to not accidentally edit

    //     // Querying & assigning sliders in cloned document
    //     m_PlayBtn = this.Q<Button>("play-button");
    //     m_CopyBtn = this.Q<Button>("copy-button");
    //     m_DeleteBtn = this.Q<Button>("delete-button");
    // }

    // private void PlaySave()
    // {
    //     Debug.Log("Playing GameFile");
    // }

    // private void CopySave()
    // {
    //     Debug.Log("Copying GameFile");
    // }

    // private void DeleteSave()
    // {
    //     Debug.Log("Deleting GameFile");
    // }
}
