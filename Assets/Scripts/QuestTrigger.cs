 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour, IPlayerTriggable
{

    public QuestManager.Objective objective;
    public QuestManager.Objective[] prerequisiteObjectives;
    public void OnPlayerTriggered(PlayerController player) {
        bool ready = true;
        foreach(QuestManager.Objective obj in prerequisiteObjectives)
        {
            if (!QuestManager.Instance.isComleted(obj)) ready = false;
        }

        if (!ready || QuestManager.Instance.isComleted(objective)) return;
        this.GetComponent<AudioSource>().Play();
        QuestManager.Instance.completeObjective(objective);
    }
}
