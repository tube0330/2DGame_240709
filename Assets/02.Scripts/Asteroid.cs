using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private string coinTag = "COIN_BULLET";
    private Transform tr;
    private float speed;

    void Start()
    {
        tr = transform;
        speed = Random.Range(3f, 10f);
    }

    void Update()
    {
        tr.Translate(Vector2.left * speed * Time.deltaTime);

        if (tr.position.x < -10)
            Destroy(gameObject);

        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(coinTag))
            Destroy(gameObject);
    }
}
