using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    private SpriteRenderer renderer;
    public Vector2 topRight;
    public Vector2 botLeft;


    private void Awake() {
        renderer = GetComponent<SpriteRenderer>();
        if (renderer != null ) {
            topRight = renderer.transform.TransformPoint(renderer.sprite.bounds.max);
            botLeft = renderer.transform.TransformPoint(renderer.sprite.bounds.min);
        }
    }
}
