using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CoinBullet : MonoBehaviour
{
    private Transform tr;
    private float speed = 1000f;
    private Rigidbody2D rb;

    void Start()
    {
        tr = transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.AddForce(transform.right * speed);

        Destroy(gameObject, 2f);
    }
}
