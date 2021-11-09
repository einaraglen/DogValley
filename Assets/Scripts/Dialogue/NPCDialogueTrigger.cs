using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour, IPlayerTriggable
{
    [TextArea]
    public string[] sentences;

    public void OnPlayerTriggered(PlayerController player) {
        //stop walk animaion
        player.animator.SetBool("isMoving", false);
        //continue dialog system
        GameController.Instance.Dialog(true);
        DialogueManager dialogueManager = DialogueManager.getInstance();
        Queue<string> dialogueQueue = new Queue<string>();
        foreach (string s in sentences) {
            dialogueQueue.Enqueue(s);
        }
        dialogueManager.startDialogue(dialogueQueue);
    }
}
