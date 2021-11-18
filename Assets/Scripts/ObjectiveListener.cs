using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Calling QuestManager.Instance.Listen is also required
public interface ObjectiveListener
{
    public void objectiveCompleted(QuestManager.Objective listener);
}
