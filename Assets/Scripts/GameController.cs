using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Paused, Dialogue }

public class GameController : MonoBehaviour {

    GameState state = GameState.FreeRoam;
    GameState previousState;
    public PlayerController playerController;
    public DialogueManager dialogueManager;

    public static GameController Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (state == GameState.FreeRoam) {
            playerController.HandleUpdate();
        }
        
        if (state == GameState.Dialogue) {
            dialogueManager.HandleUpdate();
        }
    }

    public void Dialog(bool dialog) {
        if (dialog) {
            previousState = state;
            state = GameState.Dialogue;
        } else {
            state = previousState;
        }
    }

    public void PauseGame(bool pause) {
        if (pause) {
            previousState = state;
            state = GameState.Paused;
        }
        else {
            state = previousState;
        }
    }
}
