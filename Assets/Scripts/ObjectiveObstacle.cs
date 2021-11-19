using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveObstacle : MonoBehaviour, ObjectiveListener
{
    public QuestManager.Objective killObjective = QuestManager.Objective.None;

    private void Start()
    {
        if (!QuestManager.Instance.isComleted(killObjective))
        {
            QuestManager.Instance.listenTo(killObjective, this);
        }
    }

    private void OnLevelWasLoaded()
    {
        if (QuestManager.Instance.isComleted(killObjective))
        {
            this.gameObject.SetActive(false);
        }
    }

    public void objectiveCompleted(QuestManager.Objective listenedObj)
    {
        Debug.Log("Listened");
        if (listenedObj == killObjective)
        {
            this.gameObject.SetActive(false);
            QuestManager.Instance.stopListening(killObjective, this);
        }
    }

    private void OnDestroy()
    {
        QuestManager.Instance.stopListening(killObjective, this);
    }
}
