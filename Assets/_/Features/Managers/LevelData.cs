using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField] private float _levelDuration;
    [SerializeField] private float _durationGainOnEnemyDie;

    public float m_levelDuration => _levelDuration;
    public float m_gainOnKill => _durationGainOnEnemyDie;
}