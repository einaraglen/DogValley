using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour, IPlayerTriggable
{
    [TextArea]
    public string[] sentences;

    public void OnPlayerTriggered(PlayerController player) {
        GameController.Instance.conversing(true);
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
