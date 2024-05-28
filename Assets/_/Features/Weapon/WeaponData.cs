using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Data/WeaponData")]
public class WeaponData : ScriptableObject
{
    [BoxGroup("Settings")] public int m_maxAmmo;
    [BoxGroup("Settings")] public int m_bulletCountOnShoot = 1;
    [BoxGroup("Settings")] public float m_timeBetweenShoot;
    [BoxGroup("Settings")] public float m_shootAngleRange;
    [BoxGroup("Settings")] public float m_bulletSpeed;
    [BoxGroup("Settings")] public bool m_automatic;

    [BoxGroup("Visual")] public WeaponGraphics m_weaponGraphics;
    [BoxGroup("Visual")] public Bullet m_bulletGraphics;
}