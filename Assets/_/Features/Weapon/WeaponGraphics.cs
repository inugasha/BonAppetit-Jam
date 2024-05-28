using UnityEngine;
using UnityEngine.Events;

public class WeaponGraphics : MonoBehaviour
{
    public Transform m_bulletSpawnPosition;

    public UnityEvent m_onPlayerShoot;
    public UnityEvent m_onEnemyShoot;
}