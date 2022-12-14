using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private TMPro.TextMeshProUGUI textMesh;
    private int lastFrameIndex;
    private float[] frameDeltaTimeArray;
    private void Awake()
    {
        textMesh = this.GetComponent<TextMeshProUGUI>();
        frameDeltaTimeArray = new float[50];
    }

    void Update()
    {
        frameDeltaTimeArray[lastFrameIndex] = Time.deltaTime;
        lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;
        textMesh.text = Mathf.RoundToInt(CalculateFPS()).ToString();
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
