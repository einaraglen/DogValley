using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveObstacle : MonoBehaviour, ObjectiveListener
{
    public QuestManager.Objective killObjective = QuestManager.Objective.None;
    public bool activeAfter = false;
    public GameObject before;
    public GameObject after;

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
            if (this.activeAfter)
            {
                setBefore();
            } else
            {
                setAfter();
            }
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
            if (this.activeAfter) setBefore();
            if (!this.activeAfter) setAfter();
            //this.gameObject.SetActive(activeAfter);
            QuestManager.Instance.stopListening(killObjective, this);
        }
    }

    private void setBefore()
    {
        if (this.before != null) this.before.SetActive(true);
        if (this.after != null) this.after.SetActive(false);
    }

    private void setAfter()
    {
        if (this.before != null) this.before.SetActive(false);
        if (this.after != null) this.after.SetActive(true);
    }

    private void OnDestroy()
    {
        QuestManager.Instance.stopListening(killObjective, this);
    }
}
