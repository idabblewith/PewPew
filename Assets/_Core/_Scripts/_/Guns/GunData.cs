using UnityEngine;

[CreateAssetMenu(fileName = "GunData")]
public class GunData : ScriptableObject
{
    public bool isAutomatic;
    public float timeBetweenShots = .1f;
    public float heatPerShot = 1f;
}
