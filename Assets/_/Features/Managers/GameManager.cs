using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Utils.Runtime;

public class GameManager : MonoBehaviour
{
    public static GameManager m_instance;

    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private GameObject _ui;

    [SerializeField, BoxGroup("Feedbacks")] private UnityEvent _onSceneLoaded;
    [SerializeField, BoxGroup("Feedbacks")] private UnityEvent _onSceneLoadTrigger;

    //Avoir une scene manager avec le game manager dedans
    //Si un gamemanager existe déja, detreuire l'instance de trop
    //Comme sa possible de faire play direct dans une scene
    //Et aussi possible d'assigner la ref du player en dynamique
    //Instancier le jouer? ou plutot findobjectoftype?

    //prevoir feedback avant le changement de scene
    //prevoir feedback une fois la scnee chargée
    //feedbacks doit appeler l changement de scene et le lancement du jeux une fois chargée
    //Avoir aussi un objet avec les nfos de la scene, temps max, temps kill enemy dans chaque scene , et la trouver avec findobjectoftype

    private void Awake()
    {
        if (GameManager.m_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Setup();
    }

    private void OnDestroy()
    {
        if (!_setup) return;
        _reloadLevelTimer.Stop();
        _levelDurationTimer.Stop();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Setup()
    {
        m_instance = this;
        _reloadLevelTimer = new(0, OnReloadLevelTimerOver);
        _levelDurationTimer = new(0, OnLevelDurationTimerOver);
        _levelDurationTimer.OnValueChanged += OnLevelDurationTimerValueChanged;
        SceneManager.sceneLoaded += OnSceneLoaded;
        currentGameScene = SceneManager.GetActiveScene().name;
        DontDestroyOnLoad(gameObject);
        if (currentGameScene == "Manager") ForceLoadScene("MainMenu");
        _setup = true;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        if (scene.name == "Manager" || scene.name == "MainMenu")
        {
            _ui.SetActive(false);
            return;
        }


        PlayerController player = FindObjectOfType<PlayerController>();
        if (player == null) { Debug.LogError("Player not found!"); return; }

        _player = player.transform;

        LevelData levelData = FindObjectOfType<LevelData>();
        if (levelData == null) { Debug.LogError("LevelData not found!"); return; }

        _levelDuration = levelData.m_levelDuration;
        _gainDurationOnKill = levelData.m_gainOnKill;
        _ui.SetActive(true);

        _onSceneLoaded.Invoke();
    }

    public void OnEnemyDie()
    {
        _levelDurationTimer.AddTime(_gainDurationOnKill);
    }

    public void LaunchReloadLevelTimer(float timer)
    {
        _reloadLevelTimer.ChangeTime(timer);
        _reloadLevelTimer.Start();
    }

    private void OnReloadLevelTimerOver()
    {
        _onSceneLoadTrigger.Invoke();
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

    public void LoadScene(string sceneName)
    {
        currentGameScene = string.IsNullOrWhiteSpace(sceneName) ? currentGameScene : sceneName;
        _onSceneLoadTrigger.Invoke();
    }

    public void LoadDemandedScene()
    {
        SceneManager.LoadScene(currentGameScene);
    }

    public void StartGame()
    {
        OnLevelDurationTimerValueChanged(_levelDuration);
        _levelDurationTimer.ChangeTime(_levelDuration);
        _levelDurationTimer.Start();
    }

    private void ForceLoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public Transform m_player => _player;

    private Transform _player;

    private Timer _reloadLevelTimer;
    private Timer _levelDurationTimer;

    private float _levelDuration;
    private float _gainDurationOnKill;

    private string currentGameScene;

    private bool _setup;
}