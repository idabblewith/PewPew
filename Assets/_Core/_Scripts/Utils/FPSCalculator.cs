using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class FPSCalculator : MonoBehaviour
{
    private UIDocument m_UIDocument;
    private Label m_FPSText;


    private int lastFrameIndex;
    private float[] frameDeltaTimeArray;
    private void Awake()
    {
        m_UIDocument = GetComponent<UIDocument>();        
        frameDeltaTimeArray = new float[50];
    }

    private void OnEnable() 
    {
        var root = m_UIDocument.rootVisualElement;
        m_FPSText = root.Q<Label>("fps-value");
    }

    void Update()
    {
        frameDeltaTimeArray[lastFrameIndex] = Time.deltaTime;
        lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;
        m_FPSText.text = Mathf.RoundToInt(CalculateFPS()).ToString();
    }

    private float CalculateFPS()
    {
        float total = 0f;
        foreach (float deltaTime in frameDeltaTimeArray)
        {
            total += deltaTime;
        }
        return frameDeltaTimeArray.Length /total;
    }
}
