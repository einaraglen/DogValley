using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour, IPlayerTriggable
{
    [TextArea]
    public string[] sentences;
    public QuestManager.Objective[] prerequisiteObjectives;

    public void OnPlayerTriggered(PlayerController player) {
        bool ready = true;
        foreach(QuestManager.Objective obj in prerequisiteObjectives) {
            bool hit = false;
            Debug.Log(obj.ToString());
            foreach (QuestManager.Objective objective in QuestManager.Objective.GetValues(typeof(QuestManager.Objective))) {
                Debug.Log(objective.ToString());
                if (QuestManager.Instance.isComleted(obj)) hit = true;
            }
            if (!hit) ready = false;
        }

        if (ready)
        {
            Debug.Log("Objectives fulfilled");
            //stop walk animaion
            player.animator.SetBool("isMoving", false);
            //continue dialog system
            GameController.Instance.Dialog(true);
            DialogueManager dialogueManager = DialogueManager.getInstance();
            Queue<string> dialogueQueue = new Queue<string>();
            foreach (string s in sentences)
            {
                dialogueQueue.Enqueue(s);
            }
            dialogueManager.startDialogue(dialogueQueue);
        }
    }
}
