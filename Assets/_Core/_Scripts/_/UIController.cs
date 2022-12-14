using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public TMP_Text overheatedMSG;
    public Slider weaponTempSlider;

    private void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
