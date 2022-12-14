using UnityEngine.UIElements;
using UnityEngine;

public class AboutVE : VisualElement
{
    public new class UxmlFactory:UxmlFactory<AboutVE, UxmlTraits> {}

    public AboutVE()
    {
        VisualTreeAsset visualTree = Resources.Load<VisualTreeAsset>("UI/MenuMain/AboutMenu");
        visualTree.CloneTree(this); 
    }
}