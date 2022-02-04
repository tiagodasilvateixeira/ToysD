using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int Speed;
    [SerializeField]
    private int JumpForce;
    [SerializeField]
    private Rigidbody2D Rigidbody;

    private Vector2 input;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), 0);
        transform.position = Rigidbody.position + (input * Speed * Time.deltaTime);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody.AddForce(new Vector2(0, JumpForce));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].point.x > transform.position.x && collision.gameObject.tag == "Enemy")
        {
            Rigidbody.AddForce(new Vector2(0, 500f));
        }
    }
}
