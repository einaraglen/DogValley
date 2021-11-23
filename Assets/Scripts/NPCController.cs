using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

    private Character character;
    public List<Vector2> movementPatter;
    public float timeBetweenPattern;

    float idleTimer;
    NPCState state;
    int currentPattern = 0;

    void Awake() {
        character = GetComponent<Character>();
        //StartCoroutine(character.Move(new Vector2(0, 10)));
    }

    void Update() {
        if (state == NPCState.Idle) {
            idleTimer += Time.deltaTime;
            if (idleTimer > timeBetweenPattern) {
                if (movementPatter.Count > 0) {
                    StartCoroutine(Walk());
                } else {
                    idleTimer = 0f;
                    StartCoroutine(character.Move(new Vector2(2, 0)));
                }
            }
        }
        character.HandleUpdate();
    }

    IEnumerator Walk() {
        state = NPCState.Walking;

        yield return character.Move(movementPatter[currentPattern]);
        currentPattern = (currentPattern + 1) % movementPatter.Count;

        state = NPCState.Idle;
    }
}

public enum NPCState { Idle, Walking }
