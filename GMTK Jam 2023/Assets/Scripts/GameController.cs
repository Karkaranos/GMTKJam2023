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
    [SerializeField]
    private GameObject bbq1;
    [SerializeField]
    private GameObject bbq2;
    [SerializeField]
    private GameObject bbq3;
    [SerializeField]
    private GameObject oil1;
    [SerializeField]
    private GameObject oil2;
    [SerializeField]
    private GameObject oil3;

    private int spawnTime;

    private float speed = .001f;
    Vector3 moveForce;

    Vector3 playerPos;
    Vector3 spawnPos;

    public bool isCatching = false;
    private int gameTime;

    public bool keyHeld = false;

    public bool gameWon = false;


    // Start is called before the first frame update
    void Start()
    {
        pcb = player.GetComponent<PlayerCollisionBehavior>();
        pmb = player.GetComponent<PlayerMovementBehavior>();
        kb = knife.GetComponent<KnifeBehavior>();
        StartCoroutine(CheckForPlayer());
        StartCoroutine(SauceSplots());
        StartCoroutine(Timer());
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
                if ((Mathf.Abs(playerPos.x) > 33 || (Mathf.Abs(playerPos.y) > 19.5f)) && !isCatching&&!gameWon)
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

    IEnumerator Timer()
    {
        while (pcb.lives > 0)
        {
            yield return new WaitForSeconds(1f);
            gameTime++;
            
        }
    }

    IEnumerator SauceSplots()
    {
        while (pcb.lives > 0)
        {
            yield return new WaitForSeconds(Random.Range(2,6));
            int chance = Random.Range(1, 16);
            if (chance <= (gameTime / 15))
            {
                chance = Random.Range(1, 7);
                Vector2 spawnPos;
                spawnPos.x = Random.Range(-27, 31);
                spawnPos.y = Random.Range(-18, 20);
                if (chance == 1)
                {
                    Instantiate(bbq1, spawnPos, Quaternion.identity);
                }
                if (chance == 2)
                {
                    Instantiate(bbq2, spawnPos, Quaternion.identity);
                }
                if (chance == 3)
                {
                    Instantiate(bbq3, spawnPos, Quaternion.identity);
                }
                if (chance == 4)
                {
                    Instantiate(oil1, spawnPos, Quaternion.identity);
                }
                if (chance == 5)
                {
                    Instantiate(oil2, spawnPos, Quaternion.identity);
                }
                if (chance == 6)
                {
                    Instantiate(oil3, spawnPos, Quaternion.identity);
                }
            }
        }
    }

}
