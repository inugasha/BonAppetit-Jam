using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    [SerializeField] private Transform _weaponGraphicsParent;

    public void Setup(WeaponData data)
    {
        _data = data;
        Instantiate(_data.m_weaponGraphics, _weaponGraphicsParent.position, _weaponGraphicsParent.localRotation, _weaponGraphicsParent);
    }

    public void Pickup()
    {
        Destroy(gameObject);
    }

    public WeaponData GetWeaponData() => _data;

    private WeaponData _data;
}