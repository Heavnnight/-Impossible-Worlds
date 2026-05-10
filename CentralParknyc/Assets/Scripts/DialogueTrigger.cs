using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public string npcName;
    [TextArea(3, 10)]
    public string[] dialogueLines;

    [Header("Interaction Settings")]
    public string targetTag = "Player";
    public GameObject interactPromptUI; // Assign your "Press E to Interact" UI text here

    private bool playerInRange = false;
    private bool isTalking = false;

    private void Start()
    {
        if (interactPromptUI != null)
        {
            interactPromptUI.SetActive(false); // Hide prompt initially
        }
    }

    private void Update()
    {
        // Check if player is in range and presses the interact key (E)
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isTalking)
            {
                StartConversation();
            }
            else
            {
                // If already talking, advance the dialogue
                DialogueManager.instance.DisplayNextSentence();
            }
        }
    }

    private void StartConversation()
    {
        isTalking = true;
        if (interactPromptUI != null) interactPromptUI.SetActive(false); // Hide "Press E" while talking

        DialogueManager.instance.StartDialogue(npcName, dialogueLines);
    }

    // Triggered when the player enters the collider area
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            playerInRange = true;
            if (interactPromptUI != null && !isTalking)
            {
                interactPromptUI.SetActive(true); // Show "Press E"
            }
        }
    }

    // Triggered when the player leaves the collider area
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            playerInRange = false;
            isTalking = false;

            if (interactPromptUI != null)
            {
                interactPromptUI.SetActive(false); // Hide "Press E"
            }

            DialogueManager.instance.EndDialogue(); // End dialogue if player walks away
        }
    }
}