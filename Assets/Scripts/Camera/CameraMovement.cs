using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class CameraMovement : MonoBehaviour {

    public Transform target;
    public float smoothing;

    private Vector2 maxPosition;
    private Vector2 minPosition;

    private CameraBounds bounds;
    private float height; 
    private float width;


    private void Awake() {
        Camera camera = GetComponent<Camera>();
        height = 2f * camera.orthographicSize;
        width = height * camera.aspect;
    }


    private void LateUpdate() {
        bounds = FindObjectsOfType<CameraBounds>().First();
        if (bounds != null) {

            maxPosition = new Vector2(bounds.topRight.x - width / 2, bounds.topRight.y - height / 2);
            minPosition = new Vector2(bounds.botLeft.x + width / 2, bounds.botLeft.y + height / 2);
        }

        if (transform.position != target.position) {
            //linear interpolation (move small amounts towards player)
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
         
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothing);
        }
    }

}
