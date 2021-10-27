using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosiition;

    void Start() {

    }

    //Always comes last
    private void LateUpdate() {
        if (transform.position != target.position) {
            //linear interpolation (move small amounts towards player)
            //use cameras own z pos
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosiition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosiition.y, maxPosition.y);


            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        } 
    }

}
