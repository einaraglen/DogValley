using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameState { FreeRoam, Paused, Dialogue, Locked }

public class GameController : MonoBehaviour {

    GameState state = GameState.FreeRoam;
    GameState previousState;
    public PlayerController playerController;
    public DialogueManager dialogueManager; 
    public Canvas canvas;
    private GameObject loadingScreen;
    public static bool gameCompleted = false;

    public static GameController Instance { get; private set; }

    private void Awake() {
        Instance = this;
        loadingScreen = canvas.transform.Find("LoadingScreen").gameObject;
    }

    public void completeGame()
    {
        gameCompleted = true;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame(state != GameState.Paused);
        }

        if (state == GameState.FreeRoam) {
            if (gameCompleted)
            {
                SceneManager.LoadScene(0);
            } else
            {
                playerController.HandleUpdate();
            }
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
            showPauseMenu();
        }
        else {
            state = previousState;
            hidePauseMenu();
        }
    }

    public void LockGame(bool locked) {
        if (locked) {
            //StartCoroutine(FadeLoadingScreen(true));
            previousState = state;
            state = GameState.Locked;
        }
        else {
            state = previousState;
        }
    }

    private void showPauseMenu() {
        canvas.transform.Find("PauseMenu").gameObject.SetActive(true);
    }

    private void hidePauseMenu() {
        canvas.transform.Find("PauseMenu").gameObject.SetActive(false);
    }

    void OnLevelWasLoaded() {
        //StartCoroutine(FadeLoadingScreen(false));
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public IEnumerator FadeLoadingScreen(bool faded = true, int speed = 5) {
        Color screenColor = loadingScreen.GetComponent<SpriteRenderer>().color;
        float fadeAmount;

        if (faded) {
            while (loadingScreen.GetComponent<SpriteRenderer>().color.a < 1) {
                fadeAmount = screenColor.a + (speed * Time.deltaTime);

                screenColor = new Color(screenColor.r, screenColor.g, screenColor.b, fadeAmount);
                loadingScreen.GetComponent<SpriteRenderer>().color = screenColor;
                yield return null;
            }
        } else {
            while (loadingScreen.GetComponent<SpriteRenderer>().color.a > 0) {
                fadeAmount = screenColor.a - (speed * Time.deltaTime);

                screenColor = new Color(screenColor.r, screenColor.g, screenColor.b, fadeAmount);
                loadingScreen.GetComponent<SpriteRenderer>().color = screenColor;
                yield return null;
            }
        }
        yield return new WaitForEndOfFrame();
    }

}
