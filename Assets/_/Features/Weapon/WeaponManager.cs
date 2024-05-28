using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Utils.Runtime;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponData _defaultWeaponData;
    [SerializeField] private Transform _weaponGraphicsParent;
    [SerializeField] private float _autoDestroyBulletAfter = 7;

    [SerializeField] private UnityEvent _onShoot;

    private void Awake()
    {
        _input = new Player_Actions();
        _input.Combat.Fire.started += OnFire_Started;
        _input.Combat.Fire.canceled += OnFire_Canceled;

        _shootTimer = new(0, OnShootTimerOver);
        SetupWeapon(_defaultWeaponData);

        _input.Enable();
    }

    private void OnDestroy()
    {
        _shootTimer.Stop();

        _input.Combat.Fire.started -= OnFire_Started;
        _input.Combat.Fire.canceled -= OnFire_Canceled;
    }

    private void Update()
    {
        if (!_onShooting || !_currentWeaponData.m_automatic) return;
        Shoot();
    }

    private void SetupWeapon(WeaponData data)
    {
        _canShoot = false;
        _onShooting = false;

        _currentWeaponData = data;
        _shootTimer.ChangeTime(_currentWeaponData.m_timeBetweenShoot);
        if (_weaponGraphics != null)
        {
            Destroy(_weaponGraphics.gameObject);
            _weaponGraphics = null;
        }

        _weaponGraphics = Instantiate(_currentWeaponData.m_weaponGraphics, _weaponGraphicsParent);
        _currentAmmo = _currentWeaponData.m_maxAmmo;

        _canShoot = true;
    }

    private void Shoot()
    {
        if (!_canShoot || _currentAmmo == 0) return;
        _canShoot = false;
        _currentAmmo--;

        //Instantiate
        Bullet bullet = Instantiate(_currentWeaponData.m_bulletGraphics);
        bullet.transform.position = _weaponGraphics.transform.position + _weaponGraphics.m_bulletSpawnPosition.position;
        bullet.Setup(_currentWeaponData.m_bulletSpeed, _autoDestroyBulletAfter);

        _onShoot.Invoke();

        _shootTimer.Start();
    }

    private void OnShootTimerOver()
    {
        _canShoot = true;
    }

    private void OnFire_Started(InputAction.CallbackContext context)
    {
        _onShooting = true;
        if (!_currentWeaponData.m_automatic) Shoot();
    }

    private void OnFire_Canceled(InputAction.CallbackContext context)
    {
        _onShooting = false;
    }

    private WeaponData _currentWeaponData;
    private Timer _shootTimer;
    private Player_Actions _input;
    private WeaponGraphics _weaponGraphics;

    private bool _canShoot;
    private bool _onShooting;
    private int _currentAmmo;
}