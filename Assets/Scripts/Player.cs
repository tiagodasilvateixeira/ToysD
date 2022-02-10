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
    private float RayInitPosition;
    [SerializeField]
    private float RayFinalPosition;
    [SerializeField]
    private Rigidbody2D Rigidbody;
    [SerializeField]
    private GameObject AttachLocation;

    private Vector2 input;
    private bool attachedObject;
    private List<IPlayerListener> listeners;
    public bool ropeConnection = false;

    public int Collectables { get; set; }
    public int Diamonds { get; set; }
    public int Lifes { get; set; }

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        attachedObject = false;
        Collectables = 0;
        Diamonds = 0;
        Lifes = 1;

        UpdateExternalProprierts();
    }

    private void InitializeListeners()
    {
        listeners = new List<IPlayerListener>();
    }

    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), 0);
        transform.position = transform.position + new Vector3((input * Speed * Time.deltaTime).x, 0, 0);

        if (ropeConnection)
        {
            Rigidbody.velocity = Vector2.zero;
            transform.position = new Vector3(transform.position.x, -0.8985106f, 0f);
            Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            if (Rigidbody.bodyType == RigidbodyType2D.Kinematic)
            {
                Rigidbody.bodyType = RigidbodyType2D.Dynamic;
            }
        }

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Rigidbody.bodyType == RigidbodyType2D.Dynamic)
                Rigidbody.AddForce(new Vector2(0, JumpForce));
            else
            {
                if (Input.GetAxis("Vertical") < 0f)
                {
                    ropeConnection = false;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (attachedObject)
            {
                RaycastHit2D raycastHit2D = Physics2D.Raycast(AttachLocation.transform.position, Vector2.right, 0.1f);
                if (raycastHit2D.collider != null)
                {
                    if (raycastHit2D.collider.gameObject.tag == "Circle")
                    {
                        raycastHit2D.collider.gameObject.transform.parent = null;
                        raycastHit2D.collider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        raycastHit2D.collider.GetComponent<Rigidbody2D>().AddForce(new Vector2(JumpForce/2, JumpForce));
                        attachedObject = false;
                    }
                }
            }
            else
            {
                RaycastHit2D raycastHit2D = Physics2D.Raycast(new Vector2(transform.position.x + RayInitPosition, transform.position.y), Vector2.right, RayFinalPosition);

                if (raycastHit2D.collider != null)
                {
                    if (raycastHit2D.collider.gameObject.tag == "Circle")
                    {
                        raycastHit2D.collider.gameObject.transform.SetParent(transform);
                        raycastHit2D.collider.GetComponent<Rigidbody2D>().velocity = Vector2.zero; 
                        raycastHit2D.collider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                        raycastHit2D.collider.gameObject.transform.position = AttachLocation.transform.position;
                        attachedObject = true;
                    }
                }
            }
        }
        Debug.DrawLine(new Vector3(transform.position.x + RayInitPosition, transform.position.y, 0), new Vector3(transform.position.x + RayFinalPosition, transform.position.y, 0));
    }

    public void AddToListeners(IPlayerListener playerListener)
    {
        if (listeners == null)
            InitializeListeners();
        listeners.Add(playerListener);

        UpdateExternalProprierts();
    }

    public void UpdateExternalProprierts()
    {
        foreach (IPlayerListener player in listeners)
        {
            player.UpdatePlayerProprierts(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
     {
        if (collision.contacts[0].point.x > transform.position.x)
        {
            if (collision.gameObject.tag == "Enemy")
                Rigidbody.AddForce(new Vector2(0, 500f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CapsuleCollectable")
        {
            Collectables++;
            UpdateExternalProprierts();
        }
        else if (collision.gameObject.tag == "DiamondCollectable")
        {
            Diamonds++;
            UpdateExternalProprierts();
        }
    }
}
