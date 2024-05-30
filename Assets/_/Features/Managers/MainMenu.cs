using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string _levelScenePrefix;
    public void LoadLevel(int level)
    {
        GameManager.m_instance.LoadScene(_levelScenePrefix + level);
    }
}