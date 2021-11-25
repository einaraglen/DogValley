using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum DestinationIdentifier { A, B, C, D, E, F, G }

public class Portal : MonoBehaviour, IPlayerTriggable {

    public string toSceneName;
    public Transform spawnPoint;
    public DestinationIdentifier portal;

    PlayerController player;

    public void OnPlayerTriggered(PlayerController player) {
        this.player = player;
        StartCoroutine(SwitchScene());
    }

    IEnumerator SwitchScene() {
        DontDestroyOnLoad(gameObject);
        //StartCoroutine(GameController.Instance.FadeLoadingScreen(true));
        GameController.Instance.LockGame(true);

        yield return SceneManager.LoadSceneAsync(SceneIndexFromName(toSceneName));

        //teleport player to first portal of same portal identifier A -> A etc.
        var destinationPortal = GameObject.FindObjectsOfType<Portal>().First(x => x != this && x.portal == this.portal);
        player.SetPositionAndSnapToTile(destinationPortal.spawnPoint.position);

        GameController.Instance.LockGame(false);
        //StartCoroutine(GameController.Instance.FadeLoadingScreen(false));

        Destroy(gameObject);
    }

    public Transform SpawnPoint => spawnPoint;

    private static string NameFromIndex(int BuildIndex) {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }

    private int SceneIndexFromName(string sceneName) {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
            string testedScreen = NameFromIndex(i);
            //print("sceneIndexFromName: i: " + i + " sceneName = " + testedScreen);
            if (testedScreen == sceneName)
                return i;
        }
        return -1;
    }
}
