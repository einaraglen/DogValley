using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum DestinationIdentifier { A, B, C, D}

public class Portal : MonoBehaviour, IPlayerTriggable {

    public int toSceneIndex;
    public Transform spawnPoint;
    public DestinationIdentifier portal;

    PlayerController player;

    public void OnPlayerTriggered(PlayerController player) {
        this.player = player;
        StartCoroutine(SwitchScene());
    }

    IEnumerator SwitchScene() {
        DontDestroyOnLoad(gameObject);

        GameController.Instance.LockGame(true);

        yield return SceneManager.LoadSceneAsync(toSceneIndex);

        //teleport player to first portal of same portal identifier A -> A etc.
        var destinationPortal = GameObject.FindObjectsOfType<Portal>().First(x => x != this && x.portal == this.portal);
        player.SetPositionAndSnapToTile(destinationPortal.spawnPoint.position);

        GameController.Instance.LockGame(false);

        Destroy(gameObject);
    }

    public Transform SpawnPoint => spawnPoint;
}
