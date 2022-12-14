using System;
using UnityEngine;
using UnityEngine.UIElements;

/*

    Utilising feedback from original & user trials. This version uses UIToolkit, 
    implements mobile functionality, and implements some of the design for the first prototype
    with a matrix cube for the game scene.

    This implementation is based on the Unity Opens Project "Chop Chop", Ryan Hipple's 
    talk on scriptable objects, and various Youtube tutorials by Unity and people like Brackeys.

    https://www.youtube.com/watch?v=zObWVOv1GlE&t=21s&ab_channel=Unity
    https://www.youtube.com/watch?v=aPXvoWVabPY&ab_channel=Brackeys
    https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity
    
    Uses Addressables/Scene Management/Scriptable Objects/UIDocument 
    to switch between scenes and implement USS defined styling.

    Contains delegate/callbacks for button clicked functions for each menu item.
    newgame is clicked ==> StartNewGame function 
    etc.

    Updated: 3/12/22 - Jarid Prince
*/

// Ensures there is a UI Document for the script to refer to
[RequireComponent(typeof(UIDocument))]
public class MainMenuUI : MonoBehaviour
{

    #region Variable =============================================
    
    private UIDocument m_UIDocument;
    private VisualElement m_QuitConfirmationModal;
    private AboutVE m_About;
    private SettingsVE m_Settings;
    private Label optionDescription;
    [SerializeField] private LoadSceneChannel m_LoadSceneChannel;
    [SerializeField] private SceneReference m_NewGameStartScene;
    [SerializeField] Event NewGameClickedEvent;
    #endregion

    #region UnityMethods =========================================
    private void Awake()
    {
        m_UIDocument = GetComponent<UIDocument>();
    }

    private void OnEnable() 
    {
        #region Definitions ========================================
        // Root Definitions
        var root = m_UIDocument.rootVisualElement;
        m_QuitConfirmationModal = root.Q("quit-confirmation-modal");
        m_About = root.Q<AboutVE>("about-menu");
        m_Settings = root.Q<SettingsVE>("settings-menu");
        Button quitConfirmButton = m_QuitConfirmationModal.Q<Button>("quit-confirm-button");
        Button quitCancelButton = m_QuitConfirmationModal.Q<Button>("quit-cancel-button");
        optionDescription = root.Q<Label>("option-description");

        // Base Menu Button Definitions
        var mmstuff = root.Q<VisualElement>("mm-stuff");
        var playBtn = mmstuff.Q<PlainButton>("play-button");
        var optionsBtn = mmstuff.Q<PlainButton>("options-button");
        var aboutBtn = mmstuff.Q<PlainButton>("about-button");
        var quitBtn = mmstuff.Q<PlainButton>("quit-button");
        #endregion

        #region Listeners ========================================
        // BUTTON HOVER LISTENERS

        //IN
        playBtn.RegisterCallback<MouseOverEvent>((type) =>
        {
            optionDescription.text = "Face up against others online or in a dedicated server";
        });
        optionsBtn.RegisterCallback<MouseOverEvent>((type) =>
        {
            optionDescription.text = "Adjust settings";
        });
        aboutBtn.RegisterCallback<MouseOverEvent>((type) =>
        {
            optionDescription.text = "See information about game";
        });
        quitBtn.RegisterCallback<MouseOverEvent>((type) =>
        {
            optionDescription.text = "Quit the game";
        });

        //OUT
        // playBtn.RegisterCallback<MouseOutEvent>((type) =>
        // {
        //     optionDescription.text = "Select an option";
        // });
        // optionsBtn.RegisterCallback<MouseOutEvent>((type) =>
        // {
        //     optionDescription.text = "Select an option";
        // });
        // aboutBtn.RegisterCallback<MouseOutEvent>((type) =>
        // {
        //     optionDescription.text = "Select an option";
        // });
        // quitBtn.RegisterCallback<MouseOutEvent>((type) =>
        // {
        //     optionDescription.text = "Select an option";
        // });




        // PLAY CLICK LISTENERS
        playBtn.clicked += StartGame;

        // OPTIONS CLICK LISTENERS
        optionsBtn.clicked += HandleOptionsMenuClick;       

        // ABOUT CLICK LISTENERS
        aboutBtn.clicked += HandleAboutMenuClick;

        // QUIT CLICK LISTENERS
        quitBtn.clicked += ShowConfirmationModal;
        quitConfirmButton.clicked += QuitGame;
        quitCancelButton.clicked += Cancel;
        #endregion
    }
    #endregion

    #region CustomMETHods ========================================

    private void HideAllMenus()
    {
        HideOptionsMenu();
        HideAboutMenu();
    }

    // Play
    private void StartGame()
    {
        // Debug.Log("New Game pressed");
        m_LoadSceneChannel.Load(m_NewGameStartScene);
        NewGameClickedEvent?.Raise();
    }
   
    // Options =========================================================
    private void HandleOptionsMenuClick()
    {
        if(m_Settings.style.display == DisplayStyle.None || m_Settings.style.display.ToString() == "Null")
        {
            HideAllMenus();
            ShowOptionsMenu();
            // Debug.Log("Showing");
        } 
        else if (m_Settings.style.display == DisplayStyle.Flex)
        {
            HideOptionsMenu();
            // Debug.Log("Hiding");        
        }
        else {
            Debug.Log(m_Settings.style.display);
        }
    }

    private void ShowOptionsMenu()
    {
        m_Settings.style.display = DisplayStyle.Flex;
        optionDescription.text = "Adjust settings";
    }

    private void HideOptionsMenu()
    {
        m_Settings.style.display = DisplayStyle.None;
    }

    

    // About =========================================================
    private void HandleAboutMenuClick()
    {
        if(m_About.style.display == DisplayStyle.None || m_About.style.display.ToString() == "Null")
        {
            HideAllMenus();
            ShowAboutMenu();
        } 
        else if (m_About.style.display == DisplayStyle.Flex)
        {
            HideAboutMenu();
        }
        else {
            Debug.Log(m_About.style.display);
        }
    }
    private void ShowAboutMenu()
    {        
        m_About.style.display = DisplayStyle.Flex;
        optionDescription.text = "See information about game";
    }

    private void HideAboutMenu()
    {
        m_About.style.display = DisplayStyle.None;
    }


    // Quit ========================================================
    private void ShowConfirmationModal()
    {
        m_QuitConfirmationModal.style.display = DisplayStyle.Flex;
        optionDescription.text = "Quit the game";
    }

    private void Cancel()
    {
        m_QuitConfirmationModal.style.display = DisplayStyle.None;
    }

    private void QuitGame()
    {
        m_QuitConfirmationModal.style.display = DisplayStyle.None;
        Application.Quit();
    }
    #endregion

    // #region Variables
    // private UIDocument m_UIDocument;
    // private SettingsVE m_Settings;
    // private VisualElement m_ConfirmationModal;
    // private VisualElement m_creditsPanel;
    // [SerializeField] private LoadSceneChannel m_LoadSceneChannel;
    // [SerializeField] private SceneReference m_NewGameStartScene;
    // [SerializeField] private GameData m_GameData;
    // [SerializeField] private LoadDataChannel m_LoadDataChannel;
    // #endregion


    // #region UnityFunctions
    // private void Awake() {
    //     m_UIDocument = GetComponent<UIDocument>();
    // }

    // private void OnEnable() {
    //     var root = m_UIDocument.rootVisualElement;

    //     #region NewGameStuff
    //     PlainButton newGameButton = m_UIDocument.rootVisualElement.Q<PlainButton>("new-game-button");
    //     newGameButton.clicked += StartNewGame;
    //     #endregion

    //     #region SaveGameStuff
    //         #region ContinueStuff
    //         PlainButton continueBtn = m_UIDocument.rootVisualElement.Q<PlainButton>("continue-button");
    //         PlainButton savesBtn = m_UIDocument.rootVisualElement.Q<PlainButton>("saves-button");
    //         // Shows based on existence of previous data in persistent path
    //         continueBtn.SetEnabled(m_GameData.hasPreviousSave);        // visibility depends on if previous save exists
    //         savesBtn.SetEnabled(m_GameData.hasPreviousSave);
    //         continueBtn.clicked += ContinuePreviousGame;
    //         #endregion
    //     #endregion

    //     #region CreditsStuff
    //     PlainButton creditsBtn = m_UIDocument.rootVisualElement.Q<PlainButton>("credits-button");
    //     creditsBtn.clicked += OpenCredits;
    //     m_creditsPanel = m_UIDocument.rootVisualElement.Q<VisualElement>("credits-panel");
    //     Button closeCreditsBtn = m_creditsPanel.Q<Button>("close-button");
    //     closeCreditsBtn.clicked += CloseCredits;

    //     #endregion

    //     #region QuitGameStuff
    //     PlainButton quitBtn = root.Q<PlainButton>("quit-button");
    //     quitBtn.clicked += ShowConfirmationModal;
    //     m_ConfirmationModal = root.Q("confirmation-modal");
    //     Button quitConfirmButton = m_ConfirmationModal.Q<Button>("confirm-button");
    //     quitConfirmButton.clicked += QuitGame;
    //     Button quitCancelButton = m_ConfirmationModal.Q<Button>("cancel-button");
    //     Button quitCrossButton = m_ConfirmationModal.Q<Button>("cross");
    //     quitCancelButton.clicked += Cancel;
    //     quitCrossButton.clicked += Cancel;
    //     #endregion

    //     #region SettingsStuff
    //     // m_Settings = root.Q("settings");
    //     PlainButton settingsBtn = root.Q<PlainButton>("settings-button");
    //     settingsBtn.clicked += OpenSettings;
    //     m_Settings = root.Q<SettingsVE>();
    //     Button closeSettingsBtn = m_Settings.Q<Button>("close-button");
    //     closeSettingsBtn.clicked += CloseSettings;
    //     #endregion

    // }
    // #endregion

    // #region CreditsMethods
    // private void OpenCredits()
    // {
    //     m_creditsPanel.style.display = DisplayStyle.Flex;
    // }

    // private void CloseCredits()
    // {
    //     m_creditsPanel.style.display = DisplayStyle.None;
    // }
    // #endregion

    // #region SettingsMethods
    // private void OpenSettings()
    // {
    //     Debug.Log("Settings opened");
    //     m_Settings.style.display = DisplayStyle.Flex;
    // }

    // private void CloseSettings()
    // {
    //     m_Settings.style.display = DisplayStyle.None;
    // }
    // #endregion

    // #region QuitMethods
    // private void ShowConfirmationModal()
    // {
    //     m_ConfirmationModal.style.display = DisplayStyle.Flex;
    // }

    // private void Cancel()
    // {
    //     m_ConfirmationModal.style.display = DisplayStyle.None;
    // }

    // private void QuitGame()
    // {
    //     m_ConfirmationModal.style.display = DisplayStyle.None;
    //     Application.Quit();
    // }
    // #endregion

    // private void StartNewGame()
    // {
    //     Debug.Log("New Game pressed");
    //     m_LoadSceneChannel.Load(m_NewGameStartScene);
    // }

    // // Continue Game Method
    // private void ContinuePreviousGame()
    // {
    //     m_GameData.LoadFromBinaryFile();
    //     m_LoadDataChannel.Load();
    // }

}
