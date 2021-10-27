using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Paused }

public class GameController : MonoBehaviour {

    GameState state = GameState.FreeRoam;
    GameState previousState;
    public PlayerController playerController;

    public static GameController Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (state == GameState.FreeRoam) {
            playerController.HandleUpdate();
        }
        //TODO: give game state to dialog when in dialog
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
