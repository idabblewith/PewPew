using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class IngameUI : MonoBehaviour
{
    private UIDocument m_UIDoc;
    // public event Action saved;
    // public event Action colorChanged;

    private void Awake() 
    {
        m_UIDoc = GetComponent<UIDocument>();
    }

    private void OnEnable() 
    {
        var root = m_UIDoc.rootVisualElement;
        // Button colorBtn = root.Q<Button>("color-button");
        // colorBtn.clicked += ChangeColor;
        // Button saveBtn = root.Q<Button>("save-button");
        // saveBtn.clicked += SaveGame;
    }

    private void OnDisable() 
    {
    }

    private void SaveGame()
    {
        // saved?.Invoke();
    }

    private void ChangeColor() 
    {
        // colorChanged?.Invoke();
    }
}
