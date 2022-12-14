using UnityEngine.UIElements;

/*
    Purpose: Provide a generic button without default UI Toolkit stylings
    Jarid Prince - 2/12/22
*/

public class PlainButton : Button
{
    public new class UxmlFactory : UxmlFactory<PlainButton, UxmlTraits> {}

    public PlainButton()
    {
        RemoveFromClassList("unity-button");
    }
}
