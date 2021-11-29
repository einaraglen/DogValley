using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EssentialObjects : MonoBehaviour {
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    void OnLevelWasLoaded() {
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "MainMenu") Destroy(gameObject);
    }
}
