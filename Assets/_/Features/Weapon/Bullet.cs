using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Update()
    {
        if (!_setup) return;
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_setup) return;
        //Hit
        Destroy(gameObject);
    }

    public void Setup(float bulletSpeed, float autoDestroyTime)
    {
        _speed = bulletSpeed;
        Destroy(gameObject, autoDestroyTime);
        _setup = true;
    }

    private float _speed;
    private bool _setup;
}