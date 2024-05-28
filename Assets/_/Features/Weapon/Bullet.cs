using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask _excludeLayerForPlayerBullet;
    [SerializeField] private LayerMask _excludeLayerForEnemyBullet;
    [SerializeField] private Collider _collider;

    private void Update()
    {
        if (!_setup) return;
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_setup) return;

        if (other.TryGetComponent(out HP hp))
        {
            if (hp.m_isAlive()) hp.Kill();
        }

        Destroy(gameObject);
    }

    public void Setup(float bulletSpeed, float autoDestroyTime, bool isPlayerBullet)
    {
        _collider.excludeLayers = isPlayerBullet ? _excludeLayerForPlayerBullet : _excludeLayerForEnemyBullet;
        _speed = bulletSpeed;
        Destroy(gameObject, autoDestroyTime);
        _setup = true;
    }

    private float _speed;
    private bool _setup;
}