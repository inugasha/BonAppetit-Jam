using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HP _hp;
    [SerializeField] private NavMeshAgent _agent;

    private void Start()
    {
        _hp.m_onDie += OnDie;
    }

    private void OnDestroy()
    {
        _hp.m_onDie -= OnDie;
    }

    private void OnDie()
    {
        _agent.isStopped = true;
        _agent.velocity = Vector3.zero;
    }
}