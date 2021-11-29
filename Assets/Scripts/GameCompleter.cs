using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompleter : MonoBehaviour, ObjectiveListener
{
    private QuestManager.Objective gameComplete = QuestManager.Objective.CompleteGame;
    public void objectiveCompleted(QuestManager.Objective listener)
    {
        if (listener == gameComplete)
        {
            GameController.Instance.completeGame();
        }
    }

    public void Start()
    {
        QuestManager.Instance.listenTo(gameComplete, this);
    }

    public void OnDestroy()
    {
        QuestManager.Instance.stopListening(gameComplete, this);
    }
}
