/*****************************************************************************
// File Name :         PlayerCollisionBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     July 7, 2023
//
// Brief Description : Handles player collisions, death, and respawning
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionBehavior : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private float slowdownSpeed;
    [SerializeField]
    private float speedupSpeed;
    [SerializeField]
    private float speedAdjustmentTimer;

    [SerializeField]
    private ParticleSystem seeds;
    [SerializeField]
    private ParticleSystem flesh;

    [SerializeField]
    private float respawnTime;
    [SerializeField]
    public int lives;

    private PlayerMovementBehavior pmb;
    private KnifeBehavior kb;

    private bool beenHit = false;

    [SerializeField]
    private GameObject deadPlayer;

    Rigidbody2D rb2d;

    #endregion

    #region Functions
    // Start is called before the first frame update
    void Start()
    {
        pmb = GetComponent<PlayerMovementBehavior>();
        kb = GameObject.Find("Knife").GetComponent<KnifeBehavior>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Handles collisions when colliding with a trigger
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the collision should slow down the player, reduce their speed
        if (collision.gameObject.tag == "SlowDown")
        {
            pmb.velModifier = slowdownSpeed;

            //Start a timer for when the speed returns to normal
            StartCoroutine(SpeedChangeTimer());
        }

        //If the collision should speed up the player, increase their speed
        if (collision.gameObject.tag == "SpeedUp")
        {
            pmb.velModifier = speedupSpeed;

            //Start a timer for when the speed returns to normal
            StartCoroutine(SpeedChangeTimer());
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Knife" && collision.gameObject.GetComponent<KnifeBehavior>().isFallen&&!beenHit)
        {
            beenHit = true;
            pmb.velModifier = 0;
            seeds.transform.localScale = new Vector3(1, 1, 1);
            flesh.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
            //stop animating
            StartCoroutine(Respawn());

        }
    }

    /// <summary>
    /// Handles resetting the player's speed
    /// </summary>
    /// <returns></returns>
    IEnumerator SpeedChangeTimer()
    {
        yield return new WaitForSeconds(speedAdjustmentTimer);
        pmb.velModifier = 1;
    }

    IEnumerator Respawn()
    {
        kb.playerAlive = false;
        Vector3 spawnPos = transform.position;
        Instantiate(deadPlayer, spawnPos, Quaternion.identity);
        transform.localScale = Vector3.zero;
        if (lives > 0)
        {
            lives--;
        }
        yield return new WaitForSeconds(3);
        seeds.transform.localScale = Vector3.zero;
        flesh.transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(respawnTime - 3);
        for(int i=pmb.positions.Count-1; i>=0; i--)
        {
            Vector3 destroyme = pmb.positions[i];
            pmb.positions.Remove(destroyme);
        }
        if (lives > 0)
        {
            rb2d.constraints = RigidbodyConstraints2D.None;
            transform.position = Vector3.zero;
            kb.playerAlive = true;
            pmb.velModifier = 1;
            beenHit = false;
            transform.localScale = new Vector3(1, 1, 1);
            kb.StartKnife();
        }
        if (lives <= 0)
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(3);
        }
    }

    #endregion

}
