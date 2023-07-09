/*****************************************************************************
// File Name :         KnifeBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     July 8, 2023
//
// Brief Description : Sets the knife's shadow to track the player but lag behind a
                        bit. Handles knife attacks. 
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject knifeShadow;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject knife;
    Vector3 targetPos;
    Vector3 chopTargetPos;
    [SerializeField]
    private Sprite knifeFlash;
    [SerializeField]
    private Sprite normalKnife;

    [SerializeField]
    private float timeBetweenAttacks;

    private GameController gc;

    private bool canTrack = false;
    public bool isAttacking = false;
    private float stepBetween = .05f;
    public bool isFallen = false;
    public bool playerAlive = true;

    [SerializeField]
    AudioClip knifeSound;     //Sound that plays when the knife hits the board

    /// <summary>
    /// Start is called before the first frame. Initializes knife shadow and waiting
    /// </summary>
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        knifeShadow.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
        StartCoroutine(Pause());
        StartCoroutine(Attack());
        knife.transform.localScale = Vector3.zero;
    }

    public void StartKnife()
    {
        StopAllCoroutines();
        canTrack = false;
        knifeShadow.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
        StartCoroutine(Pause());
        StartCoroutine(Attack());
        knife.transform.localScale = Vector3.zero;
    }

    /// <summary>
    /// Update is called once per frame. It sets the shadow's position to lag behind
    /// the player if it is not attacking. 
    /// </summary>
    void Update()
    {
        if (canTrack==true&&playerAlive==true&!gc.gameWon)
        {
            targetPos = player.GetComponent<PlayerMovementBehavior>().positions[0];
            player.GetComponent<PlayerMovementBehavior>().positions.Remove(targetPos);
            if (!isAttacking)
            {
                knifeShadow.transform.position = targetPos;
            }
        }
    }

    /// <summary>
    /// Sets an initial pause period before the knife starts tracking the player
    /// </summary>
    /// <returns>How long the knife pauses for</returns>
    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1.5f);
        canTrack = true;
        knifeShadow.GetComponent<Renderer>().material.color = new Color(0, 0, 0, .36f);
    }

    IEnumerator Attack()
    {
        for(; ; )
        {
            yield return new WaitForSeconds(timeBetweenAttacks);
            isAttacking = true;
            if (playerAlive&&!gc.isCatching&&!gc.gameWon)
            {
                for (float i = .36f; i < 1; i += .05f)
                {
                    knifeShadow.GetComponent<Renderer>().material.color = new Color(0, 0, 0, i);
                    yield return new WaitForSeconds(stepBetween);
                }
                chopTargetPos = knifeShadow.transform.position;
                chopTargetPos.y -= 4;
                knife.transform.position = chopTargetPos;
                knifeShadow.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
                knife.transform.localScale = new Vector3(3, 3, 3);
                for (float f = 3; f > 1.5f; f -= .1f)
                {
                    knife.transform.localScale = new Vector3(f, f, f);
                    yield return new WaitForSeconds(stepBetween);
                }
                isFallen = true;
                knife.GetComponent<SpriteRenderer>().sprite = knifeFlash;
                AudioSource.PlayClipAtPoint(knifeSound, 
                    Camera.main.transform.position);
                yield return new WaitForSeconds(.1f);
                knife.GetComponent<SpriteRenderer>().sprite = normalKnife;
                yield return new WaitForSeconds(1f);
                isFallen = false;
                for (float f = 1.5f; f < 3; f += .2f)
                {
                    knife.transform.localScale = new Vector3(f, f, f);
                    yield return new WaitForSeconds(stepBetween);
                }
                knife.transform.localScale = Vector3.zero;
                yield return new WaitForSeconds(.5f);
                /*for(float i=1; i>.36f; i -= .05f)
                {
                    knifeShadow.GetComponent<Renderer>().material.color = new Color(0, 0, 0, i);
                    yield return new WaitForSeconds(stepBetween);
                }*/
                knifeShadow.GetComponent<Renderer>().material.color = new Color(0, 0, 0, .36f);
                isAttacking = false;
            }
            else
            {
                isAttacking = false;
            }
            

        }
    }

}
