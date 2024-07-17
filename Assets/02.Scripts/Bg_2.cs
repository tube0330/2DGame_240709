using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bg_2 : MonoBehaviour
{
    Transform tr;
    BoxCollider2D box;
    float speed = 8f;
    float width;

    void Start()
    {
        tr = transform;
        box = GetComponent<BoxCollider2D>();
        width = box.size.x;
    }

    void Update()
    {
        tr.Translate(Vector2.left * speed * Time.deltaTime);

        if (tr.position.x < -width * 2)
        {
            Reposition();
        }
    }

    void Reposition()
    {
        Vector2 pos = new Vector3(width * 3-2, 0f, tr.position.z);
        tr.transform.position = pos + (Vector2)tr.position;
    }
}
