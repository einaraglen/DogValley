using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour, IPlayerTriggable
{

    public QuestManager.Objective objective;
    public void OnPlayerTriggered(PlayerController player) {
        QuestManager.Instance.completeObjective(objective);
    }
}
