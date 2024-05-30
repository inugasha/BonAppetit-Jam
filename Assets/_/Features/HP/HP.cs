using System;
using UnityEngine;
using UnityEngine.Events;

public class HP : MonoBehaviour
{
    [SerializeField] private UnityEvent _onDieFeedbacks;
    public Action<Vector3> m_onDie;

    public void Kill(Vector3 bulletDirection)
    {
        if (!_isAlive) return;
        _isAlive = false;
        m_onDie.Invoke(bulletDirection);
        _onDieFeedbacks.Invoke();
    }

    public bool m_isAlive() => _isAlive;

    private bool _isAlive = true;
}