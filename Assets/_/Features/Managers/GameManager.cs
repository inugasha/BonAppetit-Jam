using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Runtime;

public class GameManager : MonoBehaviour
{
    public static GameManager m_instance;

    [SerializeField] private Transform _player;

    private void Awake()
    {
        m_instance = this;
        _reloadLevelTimer = new(0, OnReloadLevelTimerOver);
    }

    public void LaunchReloadLevelTimer(float timer)
    {
        _reloadLevelTimer.ChangeTime(timer);
        _reloadLevelTimer.Start();
    }

    private void OnReloadLevelTimerOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public Transform m_player => _player;

    private Timer _reloadLevelTimer;
}