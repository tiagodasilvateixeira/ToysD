using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D Rigidbody;
    [SerializeField]
    private BoxCollider2D Collider;
    [SerializeField]
    private int Speed;
    [SerializeField]
    private float Range;

    private float InitialPosition;
    private float MaxLeftPosition;
    private float MaxRightPosition;
    private bool LeftDirection;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<BoxCollider2D>();

        LeftDirection = true;
        InitialPosition = transform.position.x;
        MaxLeftPosition = InitialPosition - Range;
        MaxRightPosition = InitialPosition + Range;
    }

    void Update()
    {
        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }

        if (transform.position.x <= MaxLeftPosition)
            LeftDirection = false;
        else if (transform.position.x >= MaxRightPosition)
            LeftDirection= true;

        if (LeftDirection)
            transform.position = Rigidbody.position + (new Vector2(-1, 0) * Speed * Time.deltaTime);
        else
            transform.position = Rigidbody.position + (new Vector2(1, 0) * Speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Circle")
            Hit();
        if (collision.contacts[0].point.y > transform.position.y)
            Hit();
    }

    private void Hit()
    {
        Collider.enabled = false;
        Rigidbody.AddForce(new Vector2(0, 500f));
    }
}
