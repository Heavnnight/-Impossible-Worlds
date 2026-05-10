using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToScene : MonoBehaviour
{
    [Header("Scene Settings")]
    public string sceneName;

    [Header("Player Tag")]
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}