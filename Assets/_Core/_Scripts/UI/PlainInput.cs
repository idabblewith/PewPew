using UnityEngine.UIElements;

/*
    Purpose: Provide a generic button without default UI Toolkit stylings
    Jarid Prince - 2/12/22
*/

public class PlainInput : TextField
{
    public new class UxmlFactory : UxmlFactory<PlainInput, UxmlTraits> {}

    public PlainInput()
    {
        RemoveFromClassList("unity-base-field");
        RemoveFromClassList("unity-base-text-field");
        RemoveFromClassList("unity-text-field");
    }
}
