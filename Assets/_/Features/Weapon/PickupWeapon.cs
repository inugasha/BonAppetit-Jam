using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class PickupWeapon : MonoBehaviour
{
    [SerializeField] private Transform _weaponGraphicsParent;
    [SerializeField] private UnityEvent _onPickup;
    [SerializeField] private GameObject _ui;

    [SerializeField] private bool _isLoot = false;
    [SerializeField, HideIf(nameof(_isLoot))] private WeaponData _weaponData;

    private void Awake()
    {
        if (_isLoot) return;
        Setup(_weaponData, _weaponData.m_maxAmmo);
    }

    public void Setup(WeaponData data, int ammoCount)
    {
        _data = data;
        _ammoCount = ammoCount;
        Instantiate(_data.m_weaponGraphics, _weaponGraphicsParent.position, _weaponGraphicsParent.localRotation, _weaponGraphicsParent);
    }

    public int GetAmmoCount() => _ammoCount;

    public void Pickup()
    {
        _onPickup.Invoke();
    }

    public bool m_canPickup() => _canPickup;

    public void ShowHideUI(bool show)
    {
        _ui.SetActive(show);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!other.TryGetComponent(out PickupZone zone)) return;
        zone.AddPickupWeapon(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!other.TryGetComponent(out PickupZone zone)) return;
        zone.RemovePickupWeapon(this);
        ShowHideUI(false);
    }

    public WeaponData GetWeaponData() => _data;

    private WeaponData _data;
    private bool _canPickup = true;
    private int _ammoCount;
}