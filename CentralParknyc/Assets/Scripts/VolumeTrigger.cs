using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WorldSwapTrigger : MonoBehaviour
{
    [Header("World References")]
    public GameObject normalWorld; // The normal/default world
    public GameObject darkWorld;   // The dark/creepy world

    [Header("Trigger Settings")]
    public string triggerTag = "Player"; // Tag required to activate the trigger

    private bool hasSwitched = false;

    private void Start()
    {
        // Make sure the collider works as a trigger
        GetComponent<BoxCollider>().isTrigger = true;

        // Initial state: normal world ON, dark world OFF
        if (normalWorld != null) normalWorld.SetActive(true);
        if (darkWorld != null) darkWorld.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only switch once when the Player enters
        if (!hasSwitched && other.CompareTag(triggerTag))
        {
            hasSwitched = true;

            // Switch worlds permanently
            if (normalWorld != null) normalWorld.SetActive(false);
            if (darkWorld != null) darkWorld.SetActive(true);
        }
    }
}