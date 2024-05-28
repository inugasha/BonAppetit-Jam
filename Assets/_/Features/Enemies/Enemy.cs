using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utils.Runtime;

public class Enemy : MonoBehaviour
{
    [SerializeField, BoxGroup("Components")] private HP _hp;
    [SerializeField, BoxGroup("Components")] private NavMeshAgent _agent;
    [SerializeField, BoxGroup("Components")] private Transform _weaponGraphicsParent;

    [SerializeField, BoxGroup("Patrol")] private bool _hasPatrol;
    [SerializeField, ShowIf(nameof(_hasPatrol)), BoxGroup("Patrol")] private Patrol _patrol;
    [SerializeField, ShowIf(nameof(_hasPatrol)), BoxGroup("Patrol")] private float _nextPatrolPointOffsetPosition = 0.5f;

    [SerializeField, BoxGroup("Weapon")] private UnityEvent _onShoot;
    [SerializeField, BoxGroup("Weapon")] private WeaponData _weaponData;
    [SerializeField, BoxGroup("Weapon")] private float _autoDestroyBulletAfter = 7;
    [SerializeField, BoxGroup("Weapon")] private float _waitingTimeBeforeShootPlayer;

    [SerializeField, BoxGroup("Detection")] private float _maxDetectionRange;
    [SerializeField, BoxGroup("Detection")] private float _detectionAngle;

    private void Awake()
    {
        SetupWeapon();
        SetupWeapon();

        _reloadTimer = new(_weaponData.m_reloadTime, OnReloadTimerOver);
        _shootTimer = new(_weaponData.m_timeBetweenShoot, OnShootTimerOver);
        _waitingShootTimer = new(_waitingTimeBeforeShootPlayer, OnWaitingShootTimerOver);
    }

    private void Start()
    {
        _hp.m_onDie += OnDie;
        _player = GameManager.m_instance.m_player;
    }

    private void OnDestroy()
    {
        _reloadTimer.Stop();
        _shootTimer.Stop();
        _waitingShootTimer.Stop();

        _hp.m_onDie -= OnDie;
    }

    private void Update()
    {
        if (!_hp.m_isAlive()) return;
        if (_hasPatrol) MoveToPatrolPoint();
        if (_playerCanBeShoot) Shoot();
    }

    private void FixedUpdate()
    {
        CheckIfPlayerCanBeShoot();

        if (!_playerCanBeShoot) _waitBeforeShoot = true;
        if (!_playerCanBeShoot && _waitingShootTimer.IsRunning()) _waitingShootTimer.Stop();
        if (_playerCanBeShoot && !_waitingShootTimer.IsRunning()) _waitingShootTimer.Start();
    }

    private void LookAtPlayer()
    {

    }

    private void SetupWeapon()
    {
        _weaponGraphics = Instantiate(_weaponData.m_weaponGraphics, _weaponGraphicsParent);
        _currentAmmo = _weaponData.m_maxAmmo;
    }

    private void MoveToPatrolPoint()
    {
        Vector3 targetPosition = _patrol.GetCurrentPatrolPointPosition();
        _agent.SetDestination(targetPosition);
        if (Vector3.Distance(transform.position, targetPosition) <= _nextPatrolPointOffsetPosition)
        {
            _patrol.GoToNextPatrolPoint();
        }
    }

    private void OnDie()
    {
        //TODO dropper arme par terre

        _agent.isStopped = true;
        _agent.velocity = Vector3.zero;
    }

    private void Shoot()
    {
        //TODO appliquer rotation pour que l'arme regarde le joueur, et non pas l'ennemi

        if (!_canShoot || _currentAmmo == 0 || _onReload || _waitBeforeShoot || !_hp.m_isAlive()) return;
        _canShoot = false;
        _currentAmmo--;

        for (int i = 0; i < _weaponData.m_bulletCountOnShoot; i++)
        {
            Bullet bullet = Instantiate(_weaponData.m_bulletGraphics);
            bullet.transform.position = _weaponGraphics.transform.localPosition + _weaponGraphics.m_bulletSpawnPosition.position;
            bullet.transform.rotation = _weaponGraphics.transform.rotation;
            bullet.Setup(_weaponData.m_bulletSpeed, _autoDestroyBulletAfter, false);

            if (_weaponData.m_shootAngleRange != 0)
            {
                float angle = Random.Range(-_weaponData.m_shootAngleRange, _weaponData.m_shootAngleRange);
                Quaternion additionalRotation = Quaternion.Euler(0f, angle, 0f);
                Quaternion finalRotation = bullet.transform.rotation * additionalRotation;

                bullet.transform.rotation = finalRotation;
            }
        }

        _onShoot.Invoke();
        _weaponGraphics.m_onEnemyShoot.Invoke();

        _shootTimer.Start();
        if (_currentAmmo == 0) Reload();
    }

    private void Reload()
    {
        Debug.Log("Reload");
        _onReload = true;
        _reloadTimer.Start();
    }

    private void CheckIfPlayerCanBeShoot()
    {
        //Range check
        float distance = Vector3.Distance(transform.position, _player.position);
        if (distance > _maxDetectionRange) { _playerCanBeShoot = false; return; }

        //Angle check
        Vector3 directionToPlayer = (_player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        if (angle > _detectionAngle) { _playerCanBeShoot = false; return; }

        //Raycast check
        directionToPlayer = (_player.position - _weaponGraphics.m_bulletSpawnPosition.position).normalized;
        if (!Physics.Raycast(_weaponGraphics.m_bulletSpawnPosition.position, directionToPlayer, out RaycastHit hit, _maxDetectionRange)) { _playerCanBeShoot = false; return; }
        if (!hit.collider.CompareTag("Player")) { _playerCanBeShoot = false; return; }

        _playerCanBeShoot = true;
    }

    private void OnShootTimerOver()
    {
        _canShoot = true;
    }

    private void OnReloadTimerOver()
    {
        _currentAmmo = _weaponData.m_maxAmmo;
        _onReload = false;
    }

    private void OnWaitingShootTimerOver()
    {
        _waitBeforeShoot = false;
    }

    private WeaponGraphics _weaponGraphics;
    private Transform _player;

    private Timer _waitingShootTimer;
    private Timer _shootTimer;
    private Timer _reloadTimer;

    private bool _canShoot = true;
    private bool _waitBeforeShoot = true;
    private bool _playerCanBeShoot;
    private bool _onReload;

    private int _currentAmmo;
}