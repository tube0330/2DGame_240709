using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bg_1_far : MonoBehaviour
{
    private Transform tr;
    private BoxCollider2D box;
    public float speed = 8f;
    private float width;

    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        tr = GetComponent<Transform>();
        width = box.size.x;
    }

    void Update()
    {
        tr.Translate(Vector2.left * speed * Time.deltaTime);

        if (tr.position.x <= -width * 2)
        {
            RePosition();
        }
    }

    void RePosition()
    {
        Vector2 pos = new Vector3(width * 3 - 2, 0f, tr.position.z);
        tr.position = pos + (Vector2)tr.position;
    }
}
