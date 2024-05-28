using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager m_instance;

    [SerializeField] private Transform _player;

    private void Awake()
    {
        m_instance = this;
    }

    public Transform m_player => _player;
}