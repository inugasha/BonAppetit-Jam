using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Runtime;

public class GameManager : MonoBehaviour
{
    public static GameManager m_instance;

    [SerializeField] private Transform _player;
    [SerializeField] private float _defaultTimerValue = 10f;
    [SerializeField] private float _timeGainOnEnemyDie = 2f;

    [SerializeField] private TextMeshProUGUI _timeText;

    private void Awake()
    {
        m_instance = this;
        _reloadLevelTimer = new(0, OnReloadLevelTimerOver);
        _levelDurationTimer = new(_defaultTimerValue, OnLevelDurationTimerOver);
        _levelDurationTimer.OnValueChanged += OnLevelDurationTimerValueChanged;
    }

    private void Start()
    {
        OnLevelDurationTimerValueChanged(_defaultTimerValue);
        _levelDurationTimer.Start();
    }

    private void OnDestroy()
    {
        _reloadLevelTimer.Stop();
        _levelDurationTimer.Stop();
    }

    public void OnEnemyDie()
    {
        _levelDurationTimer.AddTime(_timeGainOnEnemyDie);
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

    private void OnLevelDurationTimerOver()
    {
        Debug.Log("Timer Over!");
        OnReloadLevelTimerOver();
    }

    private void OnLevelDurationTimerValueChanged(float value)
    {
        _timeText.text = value.ToString("F2");
    }

    public Transform m_player => _player;

    private Timer _reloadLevelTimer;
    private Timer _levelDurationTimer;
}