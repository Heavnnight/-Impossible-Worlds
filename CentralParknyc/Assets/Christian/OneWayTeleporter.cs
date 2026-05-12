using UnityEngine;

public class OneWayTeleporter : MonoBehaviour
{
    [Header("Teleport Target")]
    public Transform teleportDestination;

    [Header("Tag To Teleport")]
    public string targetTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
        
            other.transform.position = teleportDestination.position;

        
            other.transform.rotation = teleportDestination.rotation;
        }
    }
}