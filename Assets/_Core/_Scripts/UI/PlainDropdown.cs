using UnityEngine.UIElements;

public class PlainDropdown : DropdownField
{
    public new class UxmlFactory : UxmlFactory<PlainDropdown, UxmlTraits> {}

    public PlainDropdown()
    {
        RemoveFromClassList("unity-base-field");
        RemoveFromClassList("unity-base-popup-field");
        RemoveFromClassList("unity-popup-field");
    }
}
