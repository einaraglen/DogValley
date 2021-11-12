using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour, IPlayerTriggable
{

    public Dialogue[] dialogues;

    [System.Serializable]
    public class Dialogue {
        [TextArea]
        public string[] sentences;
        public QuestManager.Objective[] prerequisiteObjectives;
        public bool disabled = false;
        public bool onlyOnce = false;
        public QuestManager.Objective completeObjective = QuestManager.Objective.None;
    }

    public void OnPlayerTriggered(PlayerController player) {
        int i = 0;
        int n = dialogues.Length;
        bool dialogueFound = false;
        while (i < n && !dialogueFound)
        {
            Dialogue current = dialogues[i];
            
            if (current.disabled)
            {
                i++;
                continue;
            }

            bool ready = true;
            foreach (QuestManager.Objective obj in current.prerequisiteObjectives)
            {
                bool hit = false;
                Debug.Log(obj.ToString());
                foreach (QuestManager.Objective objective in QuestManager.Objective.GetValues(typeof(QuestManager.Objective)))
                {
                    Debug.Log(objective.ToString());
                    if (QuestManager.Instance.isComleted(obj)) hit = true;
                }
                if (!hit) ready = false;
            }
            if (ready) dialogueFound = true;
            i++;
        }

        if (!dialogueFound) return;

        Dialogue dialogue = dialogues[i - 1];
        if (dialogue.onlyOnce) dialogue.disabled = true;

        Debug.Log("Objectives fulfilled");
        //stop walk animaion
        player.animator.SetBool("isMoving", false);
        //continue dialog system
        GameController.Instance.Dialog(true);
        DialogueManager dialogueManager = DialogueManager.getInstance();
        Queue<string> dialogueQueue = new Queue<string>();
        
        foreach (string s in dialogue.sentences) {
            dialogueQueue.Enqueue(s);
        }

        if (dialogue.completeObjective != QuestManager.Objective.None) {
            QuestManager.Instance.completeObjective(dialogue.completeObjective);
        }

        dialogueManager.startDialogue(dialogueQueue);
    }
}
