using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    public LayerMask solidObjectsLayer                                                                                                                                                                                                                                                            ;

    private bool noCollide = true;
    private bool isMoving;
    private Vector2 input;
    private Animator animator;
    public IEnumerator move;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (!isMoving && !DialogueManager.isConversing()) {
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
                move = Move(targetPosition);
                StartCoroutine(move);
            }
        }
        //trigger movement animation
        animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector3 targetPosition) {
        //init movment
        isMoving = true;

        //animate player between tiles
        while (((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon) && noCollide) {
            //move player based on moveSpeed
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            //yield return, stops current execution of code, but sets "checkpoint" so next execution will resume where it last yield'ed
            yield return null;
        }

        Debug.Log("Ending movement");
        //end movment
        isMoving = false;
        move = null;
    }

    private bool isWalkable(Vector3 targetPosition) {
        if (Physics2D.OverlapCircle(targetPosition, 0.2f, solidObjectsLayer) != null) return false;
        if (!noCollide)
        {
            noCollide = true;
            return false;
        }
        return true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (move != null)
        {
            StopCoroutine(move);
            isMoving = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("RoomChanger")) {
            RoomChanger roomChanger = collision.gameObject.GetComponent<RoomChanger>();

            Vector3 targetPosition = roomChanger.GetDestination();

            transform.position = targetPosition;

            if (move != null) {
                StopCoroutine(move);
                isMoving = false;
            }
        }
    }

    private void CheckForEncounters() {
        //if (Physics2D.OverlapCircle(transform.position, 0.2f, ));
    }
}
