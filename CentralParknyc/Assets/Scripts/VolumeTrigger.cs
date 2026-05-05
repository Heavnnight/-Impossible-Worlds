using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class VolumeTrigger : MonoBehaviour
{
    [Header("Target Settings")]
    [Tooltip("The GameObject or Volume you want to turn on/off.")]
    public GameObject targetVolume;

    [Tooltip("Should the target volume be active when the game starts?")]
    public bool activeOnStart = false;

    [Header("Trigger Settings")]
    [Tooltip("The tag of the object allowed to trigger this volume (usually 'Player').")]
    public string triggerTag = "Player";

    private void Start()
    {
        // Automatically ensure the BoxCollider acts as a trigger area
        GetComponent<BoxCollider>().isTrigger = true;

        // Set the initial state of the target volume
        if (targetVolume != null)
        {
            targetVolume.SetActive(activeOnStart);
        }
        else
        {
            Debug.LogWarning($"Target Volume is not assigned on the VolumeTrigger script attached to {gameObject.name}!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the correct tag
        if (other.CompareTag(triggerTag))
        {
            if (targetVolume != null)
            {
                targetVolume.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object leaving the trigger has the correct tag
        if (other.CompareTag(triggerTag))
        {
            if (targetVolume != null)
            {
                targetVolume.SetActive(false);
            }
        }
    }
}