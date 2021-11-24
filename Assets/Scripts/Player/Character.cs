using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public Animator animator;
    public LayerMask solidObjectsLayer;
    public bool isMoving;
    public float moveSpeed;
    void Awake() {
        animator = GetComponent<Animator>();

        //snap to center of nearest tile
        SetPositionAndSnapToTile(transform.position);
    }

    public void HandleUpdate() {
        //trigger movement animation
        if (animator == null) return;
        animator.SetBool("isMoving", isMoving);
    }

    public IEnumerator Move(Vector3 moveVector) {
        //set params for animator so it can act on input
        animator.SetFloat("moveX", Mathf.Clamp(moveVector.x, -1f, 1f));
        animator.SetFloat("moveY", Mathf.Clamp(moveVector.y, -1f, 1f));

        Vector3 targetPosition = transform.position;
        targetPosition.x += moveVector.x;
        targetPosition.y += moveVector.y;


        //init movment
        isMoving = true;

        //animate player between tiles
        while (((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)) {
            //if (!isWalkable(targetPosition)) yield break;
            //move player based on moveSpeed
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            //yield return, stops current execution of code, but sets "checkpoint" so next execution will resume where it last yield'ed
            yield return null;
        }

        //end movment
        isMoving = false;
    }

    public void SetPositionAndSnapToTile(Vector2 pos) {
        transform.position = new Vector3(Mathf.Floor(pos.x) + 0.5f, Mathf.Floor(pos.y) + 0.9f, transform.position.z);
    }

    private bool isWalkable(Vector3 targetPosition) {
        if (Physics2D.OverlapCircle(targetPosition, 0.2f, solidObjectsLayer) != null) return false;
        return true;
    }
}

