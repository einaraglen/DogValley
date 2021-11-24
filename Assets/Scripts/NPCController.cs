using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

    private Character character;
    public List<Vector2> movementPatter;
    public float timeBetweenPattern;
    public LayerMask solidObjectsLayer;

    float idleTimer;
    NPCState state;
    int currentPattern = 0;

    void Awake() {
        character = GetComponent<Character>();
        //StartCoroutine(character.Move(new Vector2(0, 10)));
    }

    void Update() {
        //Debug.Log(state);
        if (state == NPCState.Idle) {
            idleTimer += Time.deltaTime;
            if (idleTimer > timeBetweenPattern) {
                if (movementPatter.Count > 0) {
                    StartCoroutine("Walk");
                }
                else {
                    idleTimer = 0f;
                    StartCoroutine(character.Move(new Vector2(2, 0)));
                }
            }
        }
        character.HandleUpdate();
        CheckForPlayer();
    }

    IEnumerator Walk() {
        if (state == NPCState.Freeze) yield break;
        state = NPCState.Walking;
        yield return character.Move(movementPatter[currentPattern]);
        currentPattern = (currentPattern + 1) % movementPatter.Count;
        state = NPCState.Idle;
    }

    private void CheckForPlayer() {
        var colliders = Physics2D.OverlapCircleAll(transform.position - new Vector3(0, 0.3f), 0.2f, solidObjectsLayer);
        foreach (var collider in colliders) {
            state = NPCState.Freeze;
        }
    }
}

public enum NPCState { Idle, Walking, Freeze }
