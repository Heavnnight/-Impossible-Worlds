using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Using TextMeshPro for modern Unity UI

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance; // Singleton so other scripts can access it easily

    [Header("UI Elements")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    private Queue<string> sentences;
    private bool isTyping = false;
    private string currentSentence;

    private void Awake()
    {
        // Set up the singleton pattern
        if (instance == null) instance = this;
        else Destroy(gameObject);

        sentences = new Queue<string>();
        dialoguePanel.SetActive(false); // Hide the panel at the start
    }

    public void StartDialogue(string npcName, string[] dialogueLines)
    {
        dialoguePanel.SetActive(true);
        nameText.text = npcName;
        sentences.Clear();

        foreach (string sentence in dialogueLines)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (isTyping)
        {
            // If the player clicks while typing, finish the sentence instantly
            StopAllCoroutines();
            dialogueText.text = currentSentence;
            isTyping = false;
            return;
        }

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentSentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(currentSentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        isTyping = true;

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f); // Adjust typing speed here
        }

        isTyping = false;
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}