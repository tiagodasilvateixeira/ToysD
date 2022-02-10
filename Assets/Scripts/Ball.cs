using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D Rigidbody;
    [SerializeField]
    private CircleCollider2D Collider;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
            Hit();
    }

    private void Hit()
    {
        Collider.enabled = false;
        if (Rigidbody.bodyType == RigidbodyType2D.Kinematic)
            Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        else
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500f));
    }
}
