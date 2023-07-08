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

    private float speed = .001f;
    Vector3 moveForce;

    Vector3 playerPos;
    Vector3 spawnPos;

    private bool isCatching=false;


    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
        pcb = player.GetComponent<PlayerCollisionBehavior>();
        pmb = player.GetComponent<PlayerMovementBehavior>();
        kb = knife.GetComponent<KnifeBehavior>();
        StartCoroutine(CheckForPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
    }

    IEnumerator CheckForPlayer()
    {
        while (pcb.lives > 0)
        {
            yield return new WaitForSeconds(.5f);
            if (kb.playerAlive)
            {
                if ((Mathf.Abs(playerPos.x) > 33 || (Mathf.Abs(playerPos.y) > 19.5f))&&!isCatching){
                    yield return new WaitForSeconds(.5f);
                    if ((Mathf.Abs(playerPos.x) > 33 || (Mathf.Abs(playerPos.y) > 19.5f)) && !isCatching)
                    {
                        pmb.isCaught = true;
                        isCatching = true;
                        PawAttack();
                    }
                }

            }
        }
    }

    private void PawAttack()
    {
        spawnPos = playerPos;
        transform.position = spawnPos;
        transform.localScale = new Vector3(1, 1, 1);

        Vector3 targetPos = Vector3.zero;

        Vector3 dir = targetPos-transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //Sets the move force and speed
        moveForce.x = dir.x;
        moveForce.y = dir.y;
        moveForce *= speed;
        GetComponent<Rigidbody2D>().AddForce(moveForce);

        /*if (Mathf.Abs(transform.position.x)<20&&Mathf.Abs(transform.position.y)<15)
        {
            print("haha");
            transform.localScale = Vector3.zero;
            pmb.isCaught = false;
            isCatching = false;
        }*/

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Square")
        {
            print("haha");
            transform.localScale = Vector3.zero;
            pmb.isCaught = false;
            isCatching = false;
        }
    }
}
