using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveObstacle : MonoBehaviour, ObjectiveListener {
    public QuestManager.Objective[] killObjectives;
    public bool activeAfter = false;
    public GameObject before;
    public GameObject after;

    private void Start() {
        bool completed = true;
        foreach(QuestManager.Objective obj in killObjectives)
        {
            if (!QuestManager.Instance.isComleted(obj))
            {
                QuestManager.Instance.listenTo(obj, this);
                completed = false;
            }
        }
    }

    private void OnLevelWasLoaded() {
        bool completed = true;
        foreach (QuestManager.Objective obj in killObjectives)
        {
            if (!QuestManager.Instance.isComleted(obj))
            {
                completed = false;
            }
        }
        if (completed) {
            if (this.activeAfter) {
                setBefore();
            }
            else {
                setAfter();
            }
        }
    }

    public void objectiveCompleted(QuestManager.Objective listenedObj) {
        Debug.Log("Listened");
        foreach(QuestManager.Objective obj in killObjectives)
        {
            if (listenedObj == obj)
            {
                bool completed = true;
                foreach (QuestManager.Objective objective in killObjectives)
                {
                    if (!QuestManager.Instance.isComleted(objective))
                    {
                        completed = false;
                    }
                }
                if (completed)
                {
                    if (this.activeAfter)
                    {
                        setBefore();
                    }
                    else
                    {
                        setAfter();
                    }
                }
                QuestManager.Instance.stopListening(obj, this);
            }
        }
    }

    private void setBefore() {
        if (this.before != null) this.before.SetActive(true);
        if (this.after != null) this.after.SetActive(false);
    }

    private void setAfter() {
        if (this.before != null) this.before.SetActive(false);
        if (this.after != null) this.after.SetActive(true);
    }

    private void OnDestroy() {
        foreach (QuestManager.Objective obj in killObjectives)
        {
            QuestManager.Instance.stopListening(obj, this);
        }
    }
}
