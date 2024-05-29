using MoreMountains.Feedbacks;
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
    [SerializeField, BoxGroup("Components")] private Animator _animator;

    [SerializeField, BoxGroup("Patrol")] private bool _hasPatrol;
    [SerializeField, ShowIf(nameof(_hasPatrol)), BoxGroup("Patrol")] private Patrol _patrol;
    [SerializeField, ShowIf(nameof(_hasPatrol)), BoxGroup("Patrol")] private float _nextPatrolPointOffsetPosition = 0.5f;

    [SerializeField, BoxGroup("Weapon")] private UnityEvent _onShoot;
    [SerializeField, BoxGroup("Weapon")] private WeaponData _weaponData;
    [SerializeField, BoxGroup("Weapon")] private PickupWeapon _pickupWeaponPrefab;
    [SerializeField, BoxGroup("Weapon")] private float _autoDestroyBulletAfter = 7;
    [SerializeField, BoxGroup("Weapon")] private float _waitingTimeBeforeShootPlayer;

    [SerializeField, BoxGroup("Detection")] private float _maxDetectionRange;
    [SerializeField, BoxGroup("Detection")] private float _detectionAngle;

    [SerializeField, BoxGroup("Chase")] private float _alertDuration;
    [SerializeField, BoxGroup("Chase")] private float _rotationSpeed;

    [SerializeField, BoxGroup("Debug")] private int _resolution;
    [SerializeField, BoxGroup("Debug")] private Color _gizmoColor = Color.red;

    [SerializeField, BoxGroup("Animation")] private string _velocityParameterName;

    [SerializeField, BoxGroup("Feel")] private MMF_Player[] _mmfPlayers;

    [SerializeField, BoxGroup("Ragdoll")] private GameObject _ragdollParent;

    private void Awake()
    {
        _velocityParam = Animator.StringToHash(_velocityParameterName);
        SetupWeapon();

        _reloadTimer = new(_weaponData.m_reloadTime, OnReloadTimerOver);
        _shootTimer = new(_weaponData.m_timeBetweenShoot, OnShootTimerOver);
        _waitingShootTimer = new(_waitingTimeBeforeShootPlayer, OnWaitingShootTimerOver);
        _alertTimer = new(_alertDuration, OnAlertTimerOver);
    }

    private void Start()
    {
        _hp.m_onDie += OnDie;
        _player = GameManager.m_instance.m_player;
        SetLootAtFeedbacks();
    }

    private void OnDestroy()
    {
        _reloadTimer.Stop();
        _shootTimer.Stop();
        _waitingShootTimer.Stop();
        _alertTimer.Stop();

        _hp.m_onDie -= OnDie;
    }

    private void Update()
    {
        if (!_hp.m_isAlive()) return;

        if (_inAlert)
        {
            if (_playerInVision)
            {
                _agent.isStopped = true;
                if (_alertTimer.IsRunning()) _alertTimer.Stop();
                LookAtPlayer(_lastKnownPlayerPosition);
                Shoot();
            }
            else MoveToLastPlayerPosition();
        }
        else
        {
            if (!_positionBeforeAlertReached) BackToPositioBeforeAlert();
            else
            {
                if (_hasPatrol) MoveToPatrolPoint();
            }
        }

        UpdateAnimatorVelocity();
    }

    private void SetLootAtFeedbacks()
    {
        foreach (var player in _mmfPlayers)
        {
            foreach (var item in player.FeedbacksList)
            {
                MMF_LookAt lookat = item as MMF_LookAt;
                if (lookat == null) continue;
                lookat.LookAtTarget = _player;
            }
        }
    }

    public void EnableRagdoll()
    {
        Rigidbody[] rigidbodies = _ragdollParent.GetComponentsInChildren<Rigidbody>();
        foreach (var item in rigidbodies)
        {
            item.isKinematic = false;
        }
    }

    private void UpdateAnimatorVelocity()
    {
        float velocity = Mathf.Clamp01(_agent.velocity.magnitude / _agent.speed);
        _animator.SetFloat(_velocityParam, velocity);
    }

    private void MoveToLastPlayerPosition()
    {
        if (_agent.isStopped) _agent.isStopped = false;
        _agent.SetDestination(_lastKnownPlayerPosition);
        float distance = Vector3.Distance(transform.position, _lastKnownPlayerPosition);
        if (distance <= _nextPatrolPointOffsetPosition && !_playerInVision)
        {
            if (!_alertTimer.IsRunning()) _alertTimer.Start();
        }
    }

    private void FixedUpdate()
    {
        CalculatePlayerVisibility();

        if (!_playerInVision) _waitBeforeShoot = true;
        if (!_playerInVision && _waitingShootTimer.IsRunning()) _waitingShootTimer.Stop();
        if (_playerInVision && !_waitingShootTimer.IsRunning()) _waitingShootTimer.Start();
    }

    private void LookAtPlayer(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
    }

    private void SetupWeapon()
    {
        _weaponGraphics = Instantiate(_weaponData.m_weaponGraphics, _weaponGraphicsParent);
        _currentAmmo = _weaponData.m_maxAmmo;

        _animator.Play(_weaponData.m_holdAnimationName);
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

    private void BackToPositioBeforeAlert()
    {
        _agent.SetDestination(_lastPositionBeforeAlert);
        if (Vector3.Distance(transform.position, _lastPositionBeforeAlert) <= _nextPatrolPointOffsetPosition)
        {
            _positionBeforeAlertReached = true;
        }
    }

    private void OnDie()
    {
        PickupWeapon pickupWeapon = Instantiate(_pickupWeaponPrefab, transform.position, Quaternion.identity);
        pickupWeapon.Setup(_weaponData, _weaponData.m_maxAmmo);

        _agent.isStopped = true;
        _agent.velocity = Vector3.zero;

        GameManager.m_instance.OnEnemyDie();
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
        _animator.Play(_weaponData.m_fireAnimationName);

        _shootTimer.Start();
        if (_currentAmmo == 0) Reload();
    }

    private void Reload()
    {
        Debug.Log("Reload");
        _onReload = true;
        _reloadTimer.Start();
    }

    private void CalculatePlayerVisibility()
    {
        //Range check
        float distance = Vector3.Distance(transform.position, _player.position);
        if (distance > _maxDetectionRange) { _playerInVision = false; return; }

        //Angle check
        Vector3 directionToPlayer = (_player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        if (angle > _detectionAngle / 2) { _playerInVision = false; return; }

        //Raycast check
        directionToPlayer = (_player.position - _weaponGraphics.m_bulletSpawnPosition.position).normalized;
        if (!Physics.Raycast(_weaponGraphics.m_bulletSpawnPosition.position, directionToPlayer, out RaycastHit hit, _maxDetectionRange)) { _playerInVision = false; return; }
        if (!hit.collider.CompareTag("Player")) { _playerInVision = false; return; }

        if (!_inAlert && _positionBeforeAlertReached)
        {
            _lastPositionBeforeAlert = transform.position;
            _positionBeforeAlertReached = false;
        }
        _inAlert = true;
        _playerInVision = true;
        _lastKnownPlayerPosition = _player.position;
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

    private void OnAlertTimerOver()
    {
        _inAlert = false;
    }

    private void OnDrawGizmos()
    {
        DrawVisionMesh();
    }

    private void DrawVisionMesh()
    {
        if (_visionMesh == null) { _visionMesh = new(); }

        Vector3[] vertices = new Vector3[_resolution + 2];
        int[] triangles = new int[_resolution * 3];

        vertices[0] = Vector3.zero;

        float halfViewAngle = _detectionAngle / 2f;
        for (int i = 0; i <= _resolution; i++)
        {
            float angle = (i / (float)_resolution) * _detectionAngle - halfViewAngle;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            Vector3 vertex = direction * _maxDetectionRange;

            if (float.IsNaN(vertex.x) || float.IsNaN(vertex.y) || float.IsNaN(vertex.z) ||
                float.IsInfinity(vertex.x) || float.IsInfinity(vertex.y) || float.IsInfinity(vertex.z))
            {
                Debug.LogError("Invalid vertex detected");
                return;
            }

            vertices[i + 1] = vertex;
        }

        for (int i = 0; i < _resolution; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        _visionMesh.Clear();
        _visionMesh.vertices = vertices;
        _visionMesh.triangles = triangles;
        _visionMesh.RecalculateNormals();

        Gizmos.color = _gizmoColor;
        Gizmos.DrawMesh(_visionMesh, transform.position, transform.rotation);
    }

    private WeaponGraphics _weaponGraphics;
    private Transform _player;
    private Mesh _visionMesh;
    private Vector3 _lastKnownPlayerPosition;
    private Vector3 _lastPositionBeforeAlert;

    private Timer _waitingShootTimer;
    private Timer _shootTimer;
    private Timer _reloadTimer;
    private Timer _alertTimer;

    private bool _canShoot = true;
    private bool _waitBeforeShoot = true;
    private bool _playerInVision;
    private bool _onReload;
    private bool _inAlert;
    private bool _positionBeforeAlertReached = true;

    private int _currentAmmo;
    private int _velocityParam;
}