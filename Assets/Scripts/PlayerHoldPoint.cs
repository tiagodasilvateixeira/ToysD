using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHoldPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerGameObject;

    private Player Player;

    void Start()
    {
        Player = PlayerGameObject.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RopeIN")
        {
            Player.ropeConnection = true;
        }
        else if (collision.gameObject.tag == "RopeOUT")
        {
            Player.ropeConnection = false;
        }
    }
}
