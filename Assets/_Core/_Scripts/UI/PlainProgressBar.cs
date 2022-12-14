using UnityEngine.UIElements;

public class PlainProgressBar : ProgressBar
{
    public new class UxmlFactory : UxmlFactory<PlainProgressBar, UxmlTraits> {}

    public PlainProgressBar()
    {
        RemoveFromClassList("unity-progress-bar");
    }
}
