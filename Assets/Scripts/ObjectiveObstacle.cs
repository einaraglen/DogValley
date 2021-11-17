using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveObstacle : MonoBehaviour
{
    public int myLevel;
    public QuestManager.Objective killObjective = QuestManager.Objective.None;

    private void OnLevelWasLoaded(int level)
    {
        if (QuestManager.Instance.isComleted(killObjective)) this.gameObject.SetActive(false);
    }
}
