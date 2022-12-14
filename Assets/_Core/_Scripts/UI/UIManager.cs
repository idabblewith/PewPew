using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    // Input
    [SerializeField] private InputReader m_InputReader;
    [SerializeField] private PauseMenuUI m_PauseMenu;
    // [SerializeField] private IngameUI m_IngameUI;
    // [SerializeField] private GameData m_GameData;
    // [SerializeField] private SaveDataChannel m_SaveDataChannel;
    [SerializeField] private SceneReference m_MainMenuScene;
    [SerializeField] private LoadSceneChannel m_LoadSceneChannel;


    private void OnEnable() 
    {
        m_InputReader.paused += OnPause;
        m_InputReader.unpaused += OnUnpause;
        m_PauseMenu.unpaused += OnUnpause;
        m_PauseMenu.openedMainMenu += BackToMenu;
    }

    private void OnDisable() 
    {
        m_InputReader.paused -= OnPause;
        m_InputReader.unpaused -= OnUnpause;
        m_PauseMenu.unpaused -= OnUnpause;
        m_PauseMenu.openedMainMenu -= BackToMenu;
    }

    private void OnPause()
    {
        if(!SceneManager.GetSceneByName("MenuMain").isLoaded)
        {
            // IF MENU SCENE NOT OPEN - ensures pause menu only pops up in-game
            // Debug.Log("UI Manager: Game Paused");
            m_PauseMenu.gameObject.SetActive(true);
            m_InputReader.EnablePauseMenuInput();
            //
        }
    }

    private void OnUnpause()
    {
        if(!SceneManager.GetSceneByName("MenuMain").isLoaded)
        {
            // IF MENU SCENE NOT OPEN - ensures pause menu only pops up in-game
            // Debug.Log("UI Manager: Game Resumed");
            m_PauseMenu.gameObject.SetActive(false);
            m_InputReader.EnableGameplayInput();
        }
    }

    private void BackToMenu()
    {
        // Save();
        m_LoadSceneChannel.Load(m_MainMenuScene);
        OnUnpause();
        m_InputReader.EnableMenuInput();
    }

    // private void Save()
    // {
    //     m_GameData.LoadFromBinaryFile(); // Loaded first in case gamedata dict not previously populated
    //     m_SaveDataChannel.Save();       
    //     m_GameData.SaveToBinaryFile();
    // }
}
