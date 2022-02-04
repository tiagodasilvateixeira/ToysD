using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D Rigidbody;
    [SerializeField]
    private BoxCollider2D Collider;
    [SerializeField]
    private int Speed;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        transform.position = Rigidbody.position + (new Vector2(-1, 0) * Speed * Time.deltaTime);

        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].point.y > transform.position.y)
        {
            Collider.enabled = false;
            Rigidbody.AddForce(new Vector2(0, 500f));
        }
    }
}
