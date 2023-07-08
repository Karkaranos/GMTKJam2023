using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject knife;
    PlayerCollisionBehavior pcb;
    PlayerMovementBehavior pmb;
    KnifeBehavior kb;
    [SerializeField]
    private GameObject paw;

    private float speed = .001f;
    Vector3 moveForce;

    Vector3 playerPos;
    Vector3 spawnPos;

    public bool isCatching = false;


    // Start is called before the first frame update
    void Start()
    {
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
                if ((Mathf.Abs(playerPos.x) > 33 || (Mathf.Abs(playerPos.y) > 19.5f)) && !isCatching)
                {
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
        var newObj=Instantiate(paw, spawnPos, Quaternion.identity);

        Vector3 targetPos = Vector3.zero;
        Vector3 dir;
        dir = targetPos - playerPos;
        float angle = ((Mathf.Atan2(dir.y, dir.x))* Mathf.Rad2Deg)-90;
        newObj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        //Sets the move force and speed
        moveForce.x = dir.x;
        moveForce.y = dir.y;
        moveForce *= speed;
        newObj.GetComponent<Rigidbody2D>().AddForce(moveForce);
    }

}
