using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsRoller : MonoBehaviour
{
    public float scrollSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = gameObject.transform.localPosition;
        var nextPos = new Vector3(currentPos.x, currentPos.y + Time.deltaTime * scrollSpeed);
        //var pos = new Vector2(0f, Mathf.Sin(Time.time * 10f) * 100f);
        this.transform.localPosition = nextPos;
    }
}
