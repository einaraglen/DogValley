using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveObstacle : MonoBehaviour, ObjectiveListener
{
    public QuestManager.Objective killObjective = QuestManager.Objective.None;
    public bool activeAfter = false;

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
            this.gameObject.SetActive(activeAfter);
        }
    }

    public void objectiveCompleted(QuestManager.Objective listenedObj)
    {
        Debug.Log("Listened");
        if (listenedObj == killObjective)
        {
            //check if obstacle has sprite render and activeAfter is true
            if (this.GetComponent<SpriteRenderer>() != null && activeAfter) {
                this.GetComponent<SpriteRenderer>().enabled = true;
            }
            this.gameObject.SetActive(activeAfter);
            QuestManager.Instance.stopListening(killObjective, this);
        }
    }

    private void OnDestroy()
    {
        QuestManager.Instance.stopListening(killObjective, this);
    }
}
