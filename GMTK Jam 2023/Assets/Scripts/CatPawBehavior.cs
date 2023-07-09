/*****************************************************************************
// File Name :         CatPawBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     July 7, 2023
//
// Brief Description : Controls the Cat Paw, which scoops the player onto the board
                        if they try to escape
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPawBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject knife;
    PlayerCollisionBehavior pcb;
    PlayerMovementBehavior pmb;
    KnifeBehavior kb;
    GameController gc;

    private float speed = .001f;
    Vector3 moveForce;

    Vector3 playerPos;
    Vector3 spawnPos;

    private bool isCatching=false;


    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        player = GameObject.Find("Tomato");
        pcb = player.GetComponent<PlayerCollisionBehavior>();
        pmb = player.GetComponent<PlayerMovementBehavior>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Square")
        {
            print("haha");
            transform.localScale = Vector3.zero;
            pmb.isCaught = false;
            gc.isCatching = false;
            Destroy(gameObject);
        }
    }
}
