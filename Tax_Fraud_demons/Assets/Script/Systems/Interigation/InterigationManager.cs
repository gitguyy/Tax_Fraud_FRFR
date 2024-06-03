using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterigationManager : MonoBehaviour
{
    public List<string> dialogues;
    private int currentDialogueIndex = 0;
    public Text dialogueText;

    void Start()
    {
        StartDialogue();
    }

    public void StartDialogue()
    {
        currentDialogueIndex = 0;
        ShowCurrentDialogue();
    }

    public void NextDialogue()
    {
        currentDialogueIndex = (currentDialogueIndex + 1) % dialogues.Count;
        ShowCurrentDialogue();
    }

    public void ShowCurrentDialogue()
    {
        dialogueText.text = dialogues[currentDialogueIndex];
    }
}