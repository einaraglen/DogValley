using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerTriggable  {
    //called when player hits IPlayerTriggable
    void OnPlayerTriggered(PlayerController player);
}
