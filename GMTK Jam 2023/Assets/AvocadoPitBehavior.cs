using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvocadoPitBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject knife;
    private KnifeBehavior kb;
    Rigidbody2D rb2d;
    [SerializeField]
    private GameObject player;
    private PlayerMovementBehavior pmb;
    private GameController gc;

    public bool isFree=false;
    public bool isOnKnife = false;
    public bool isWithPlayer=false;
    Vector3 followPos;
    private bool isFalling = false;

    private float distance;
    Vector3 playerOffset;
    Vector3 pitRespawnSpot;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        kb = knife.GetComponent<KnifeBehavior>();
        rb2d=GetComponent<Rigidbody2D>();
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        pmb = player.GetComponent<PlayerMovementBehavior>();
        pitRespawnSpot = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnKnife)
        {
            followPos = knife.transform.position;
            followPos.y += distance;
            transform.position = followPos;
            if (kb.isAttacking == false&&!isFalling)
            {
                StartCoroutine(PitDrop());
                isFalling = true;
            }
        }
        if (isWithPlayer&&pmb.isCaught==false)  
        {
            followPos = player.transform.position;
            followPos.x += playerOffset.x;
            followPos.y += playerOffset.y;
            transform.position = followPos;
        }
        if (isWithPlayer && pmb.isCaught == true)
        {
            ResetPit();
        }

        if (gc.gameWon)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isFree && !isOnKnife)
        {
            if (collision.gameObject.tag == "Knife" && collision.gameObject.GetComponent<KnifeBehavior>().isFallen)
            {
                isFree = true;
                isOnKnife = true;
                print("hit");
                StartCoroutine(FollowKnife());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFree && !isOnKnife && !isWithPlayer)
        {
            if (collision.gameObject.name == "Tomato")
            {
                isWithPlayer = true;
                gc.keyHeld = true;
                playerOffset.x = transform.position.x - collision.transform.position.x;
                playerOffset.y = transform.position.y - collision.transform.position.y;
            }
        }
    }


    IEnumerator PitDrop()
    {
        isOnKnife = false;
        for (float i = 5f; i > 4; i -= .1f)
        {
            yield return new WaitForSeconds(.1f);
            transform.localScale = new Vector3(i, i, i);
        }
    }

    IEnumerator FollowKnife()
    {
        distance = transform.position.y-knife.transform.position.y;
        yield return new WaitForSeconds(1f);
        for (float i = 4; i < 5; i+=.1f)
        {
            yield return new WaitForSeconds(.05f);
            transform.localScale = new Vector3(i, i, i);
        }
        rb2d.constraints = RigidbodyConstraints2D.None;

    }

    private void ResetPit()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.position = pitRespawnSpot;
        isFree = false;
        isOnKnife = false;
        isWithPlayer = false;
        isFalling = false;
        gc.keyHeld = false;
    }
}
