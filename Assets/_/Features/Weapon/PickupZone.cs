using System.Collections.Generic;
using UnityEngine;

public class PickupZone : MonoBehaviour
{
    private void Update()
    {
        GetNearestWeapon();
    }

    public void AddPickupWeapon(PickupWeapon pickup)
    {
        _pickupWeapons.Add(pickup);
    }

    public void RemovePickupWeapon(PickupWeapon pickup)
    {
        _pickupWeapons.Remove(pickup);
        if (pickup == _nearestWeapon) _nearestWeapon = null;
        if (_pickupWeapons.Count == 0) _nearestWeapon = null;
        GetNearestWeapon();
    }

    public WeaponData PickupWeapon()
    {
        if (_nearestWeapon == null || !_nearestWeapon.m_canPickup()) return null;
        _pickupWeapons.Remove(_nearestWeapon);
        WeaponData data = _nearestWeapon.GetWeaponData();
        _nearestWeapon.Pickup();
        _nearestWeapon = null;

        return data;
    }

    private void GetNearestWeapon()
    {
        if (_pickupWeapons.Count == 0) return;

        float distance = float.MaxValue;
        int index = 0;
        for (int i = 0; i < _pickupWeapons.Count - 1; i++)
        {
            float currentDistance = Vector3.Distance(transform.position, _pickupWeapons[i].transform.position);
            if (currentDistance < distance)
            {
                index = i;
            }
        }

        if (_nearestWeapon != null) _nearestWeapon.ShowHideUI(false);
        _nearestWeapon = _pickupWeapons[index];
        _nearestWeapon.ShowHideUI(true);
    }

    private List<PickupWeapon> _pickupWeapons = new();
    private PickupWeapon _nearestWeapon;
}