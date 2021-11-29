using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EssentialObjectsSpawner : MonoBehaviour {
    public GameObject essentialObjectsPrefab;

    private void Awake() {
        var existingObjects = FindObjectsOfType<EssentialObjects>();
        if (existingObjects.Length == 0) {
            Instantiate(essentialObjectsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        } 
    }
}
