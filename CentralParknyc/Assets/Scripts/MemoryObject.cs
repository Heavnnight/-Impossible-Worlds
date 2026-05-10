using UnityEngine;
using System.Collections;

public class MemoryObject : MonoBehaviour
{
    [Header("Object To Hide")]
    public GameObject objectToHide;

    [Header("Settings")]
    public float disappearDelay = 0.2f;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(DisappearSequence());
        }
    }

    IEnumerator DisappearSequence()
    {
        yield return new WaitForSeconds(disappearDelay);

        if (objectToHide != null)
        {
            objectToHide.SetActive(false);
        }
    }
}