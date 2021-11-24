using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {
    // To add objectives, add an entry to the enum
    public enum Objective {
        None,
        TTStatue,
        FindSecret,
        FindEntrance,
        OpenEntrance,
        RemoveBones1,
        RemoveBones2,
        RemoveDone1,
        RemoveBones3,
        RemoveBones4,
        RemoveDone2,
        InitialForestMonolog,
        OpenChest,
        ChestMonolog,
    }


    private Dictionary<Objective, List<ObjectiveListener>> listeners;
    private Dictionary<Objective, bool> progress;
    public static QuestManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
        Instance.setupObjectives();

    }

    public void completeObjective(Objective objective) {
        progress[objective] = true;
        List<ObjectiveListener> ltnrs = new List<ObjectiveListener>();
        foreach (ObjectiveListener ltnr in listeners[objective]) {
            ltnrs.Add(ltnr);
        }
        foreach (ObjectiveListener ltnr in ltnrs) {
            ltnr.objectiveCompleted(objective);
        }
    }

    public void Start() {
    }

    public bool isComleted(Objective objective) {
        return progress[objective];
    }

    public void stopListening(Objective objective, ObjectiveListener listener) {
        this.listeners[objective].Remove(listener);
    }

    public void listenTo(Objective objective, ObjectiveListener listener) {
        Debug.Log("AddListener");
        this.listeners[objective].Add(listener);
    }

    private void setupObjectives() {
        listeners = new Dictionary<Objective, List<ObjectiveListener>>();
        progress = new Dictionary<Objective, bool>();
        foreach (Objective obj in (Objective[])Objective.GetValues(typeof(Objective))) {
            progress.Add(obj, false);
            listeners.Add(obj, new List<ObjectiveListener>());
        }
        progress[Objective.None] = true;
    }


}
