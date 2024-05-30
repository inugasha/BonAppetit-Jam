using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string _levelScenePrefix;
    public void LoadLevel(int level)
    {
        GameManager.m_instance.ChangeScene(_levelScenePrefix + level);
    }
}