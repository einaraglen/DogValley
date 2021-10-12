using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    public LayerMask solidObjectsLayer                                                                                                                                                                                                                                                            ;

    private bool isMoving;
    private Vector2 input;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (!isMoving) {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //only travle in one axis at the time
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero) {
                //set params for animator so it can act on input
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                Vector3 targetPosition = transform.position;
                targetPosition.x += input.x;
                targetPosition.y += input.y;
                //cancel walking if targetPosition is in an unWalkable position
                if (!isWalkable(targetPosition)) return;
                //start movment towards given target on new thread
                StartCoroutine(Move(targetPosition));
            }
        }
        //trigger movement animation
        animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector3 targetPosition) {
        //init movment
        isMoving = true;

        //animate player between tiles
        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon) {
            //move player based on moveSpeed
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            //yield return, stops current execution of code, but sets "checkpoint" so next execution will resume where it last yield'ed
            yield return null;
        }

        //if less than Epsilon is left of distance, move player to new tile
        transform.position = targetPosition;

        //end movment
        isMoving = false;
    }

    private bool isWalkable(Vector3 targetPosition) {
        if (Physics2D.OverlapCircle(targetPosition, 0.2f, solidObjectsLayer) != null) return false;
        return true;
    }

    /*private void CheckForEncounters() {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, ))
    }*/
}
