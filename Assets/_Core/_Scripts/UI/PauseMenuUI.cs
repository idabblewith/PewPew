using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class PauseMenuUI : MonoBehaviour
{
    private UIDocument m_UIDoc;
    
    public event Action unpaused;
    public event Action openedMainMenu;

    private void Awake() 
    {
        m_UIDoc = GetComponent<UIDocument>();
    }

    private void OnEnable() 
    {
        var root = m_UIDoc.rootVisualElement;
        Button continueBtn = root.Q<Button>("continue-button");
        continueBtn.clicked += Continue;
        Button backToMenuBtn = root.Q<Button>("main-menu-button");
        backToMenuBtn.clicked += OpenMainMenu;
        Time.timeScale = 0;
    }

    private void OpenMainMenu()
    {
        openedMainMenu?.Invoke();
    }

    private void OnDisable() 
    {
        Time.timeScale = 1;
    }

    private void Continue() 
    {
        unpaused?.Invoke();
    }
}
