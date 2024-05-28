using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private Color _patrolPathColor = Color.blue;

    public Vector3 GetCurrentPatrolPointPosition() => _patrolPoints[_patrolPointIndex].position;

    public void GoToNextPatrolPoint()
    {
        _patrolPointIndex++;
        _patrolPointIndex %= _patrolPoints.Length;
    }

    public int GetNearPatrolPointIndex(Vector3 position)
    {
        int index = 0;
        float distance = float.MaxValue;

        for (int i = 0; i < _patrolPoints.Length; i++)
        {
            float currentDistance = Vector3.Distance(_patrolPoints[i].position, position);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                index = i;
            }
        }

        return index;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _patrolPathColor;
        if (_patrolPoints.Length < 2) return;
        for (int i = 0; i < _patrolPoints.Length - 1; i++)
        {
            if (_patrolPoints[i] == null || _patrolPoints[i + 1] == null) continue;
            Gizmos.DrawLine(_patrolPoints[i].position, _patrolPoints[i + 1].position);
        }

        Gizmos.DrawLine(_patrolPoints[_patrolPoints.Length - 1].position, _patrolPoints[0].position);
    }

    private int _patrolPointIndex;
}