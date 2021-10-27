using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IPlayerTriggable {

    public int toSceneIndex;

    public void OnPlayerTriggered(PlayerController player) {
        StartCoroutine(SwitchScene());
    }

    IEnumerator SwitchScene() {
        yield return SceneManager.LoadSceneAsync(toSceneIndex);
    }
}
