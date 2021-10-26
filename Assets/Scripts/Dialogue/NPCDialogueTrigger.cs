using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    [TextArea]
    public string[] sentences;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Dialogue triggered");
        DialogueManager dialogueManager = DialogueManager.getInstance();
        Debug.Log("Got mono");
        Queue<string> dialogueQueue = new Queue<string>();
        foreach (string s in sentences)
        {
            dialogueQueue.Enqueue(s);
        }
        Debug.Log("Starting dia");
        dialogueManager.startDialogue(dialogueQueue);
        Debug.Log("Started dia");
    }
}
