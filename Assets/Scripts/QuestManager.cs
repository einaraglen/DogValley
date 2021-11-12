using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // To add objectives, add an entry to the enum
    public enum Objective {TTFirstMan, FindBone, EnterHouse, FindLog, None}

    private Dictionary<Objective, bool> progress;
    public static QuestManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void completeObjective(Objective objective) {
        progress[objective] = true;
    }

    public void Start()
    {
        Instance.setupObjectives();
    }

    public bool isComleted(Objective objective) {
        return progress[objective];
    }

    private void setupObjectives() {
        progress = new Dictionary<Objective, bool>();
        foreach (Objective obj in (Objective[]) Objective.GetValues(typeof(Objective))) {
            progress.Add(obj, false);
        }
        progress[Objective.None] = true;
    }


}
