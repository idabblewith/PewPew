using UnityEngine;

public class Gun : MonoBehaviour
{
    [field: SerializeField] public GunData gunData {get; private set;}
    public bool isAutomatic;
    public float timeBetweenShots =.1f, heatPerShot = 1f;
    public GameObject muzzleFlash;

    private void Awake() {
        PullData();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void PullData()
    {
        isAutomatic = gunData.isAutomatic;
        timeBetweenShots = gunData.timeBetweenShots;
        heatPerShot = gunData.heatPerShot;
    }
}
