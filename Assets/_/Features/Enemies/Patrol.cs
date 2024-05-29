using UnityEditor;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private Color _patrolPathColor = Color.blue;
    [SerializeField] private float _pathThickness = 3f;

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
        if (_patrolPoints.Length < 2) return;
        Vector3 point1;
        Vector3 point2;

        for (int i = 0; i < _patrolPoints.Length - 1; i++)
        {
            if (_patrolPoints[i] == null || _patrolPoints[i + 1] == null) continue;
            point1 = _patrolPoints[i].position;
            point2 = _patrolPoints[i + 1].position;
            Handles.DrawBezier(point1, point2, point1, point2, _patrolPathColor, null, _pathThickness);
        }

        point1 = _patrolPoints[_patrolPoints.Length - 1].position;
        point2 = _patrolPoints[0].position;

        Handles.DrawBezier(point1, point2, point1, point2, _patrolPathColor, null, _pathThickness);
    }

    private int _patrolPointIndex;
}