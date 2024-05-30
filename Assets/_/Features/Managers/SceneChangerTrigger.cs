using UnityEngine;

public class SceneChangerTrigger : MonoBehaviour
{
    [SerializeField] private string _targetedSceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (string.IsNullOrWhiteSpace(_targetedSceneName))
        {
            Debug.LogError("Scene name is empty");
            return;
        }

        if (!other.TryGetComponent(out PlayerController playerController)) return;
        if (!other.TryGetComponent(out WeaponManager weaponManager)) return;

        playerController.enabled = false;
        weaponManager.enabled = false;

        GameManager.m_instance.LoadScene(_targetedSceneName);
    }
}