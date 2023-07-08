/*****************************************************************************
// File Name :         PlayerMovementBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     July 7, 2023
//
// Brief Description : Creates initial player direction, handles player input and 
                        momentum. 
*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehavior : MonoBehaviour
{
    #region Variables

    [SerializeField]
    [Range(0, 2)]
    private float maxSpeed;         //Player's max speed
    [SerializeField]
    private float speed;            //Player's current speed
    [SerializeField]
    private float forceAmt;         //Force added for a key being down
    [SerializeField]
    private float angleAmt;         //Force added to angle

    [SerializeField]
    private float playableAreaWidth;    //Gets game's width
    [SerializeField]
    private float playableAreaHeight;   //Gets game's height
    private Vector2 clampedPos;         //Player position clamped within game bounds

    Rigidbody2D rb2d;               //Reference to player's rigidbody
    [SerializeField] 
    private float driftTime;        //How long it takes for player to slow down
    [SerializeField]
    [Range (0,1)]
    private float slowdown;         //What percent of speed the player keeps while
                                    //slowing down (100%= no change, 0% = full stop)
    public bool isWaiting = false;      //Whether player is currently slowing
    private int wait;                   //Checks for current input
    public bool canSlow = false;        //Only allows slow down after first input
    Vector2 vel;                    //Player's velocity
    private float angle;
    public float velModifier = 1;

    private PlayerCollisionBehavior pcb;
    private KnifeBehavior kb;

    private float value;

    public List<Vector3> positions = new List<Vector3>();
    public bool isCaught = false;

    [SerializeField]
    private GameObject paw;

    #endregion

    #region Functions

    /// <summary>
    /// Start is called on the first frame. It initializes the player's momentum and
    /// direction
    /// </summary>
    void Start()
    {
        vel = new Vector2(5f, 0.0001f);
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = vel;
        pcb = GetComponent<PlayerCollisionBehavior>();
        kb = GameObject.Find("Knife").GetComponent<KnifeBehavior>();
    }


    /// <summary>
    /// Update is called every frame and handles clamping, as well as moving the 
    /// tomato and slowing down. 
    /// </summary>
    void Update()
    {
        //Gets the input direction from the player and adds force
        GetInput();

        //If no input and after first input of game and not slowing, slow it
        if (isWaiting == false&&wait==4&&canSlow)
        {
            isWaiting = true;
            //Slow down the player until they stop
            StartCoroutine(LingeringVelocity());
        }

        //Clamps the player within the game area
        ClampPlayer();

    }

    /// <summary>
    /// This function gets input from the player using the arrow keys and adds force
    /// to the player. The longer the key is pressed, the more force added. 
    /// </summary>
    private void GetInput()
    {
        value = 0;
        //Resets the input checker
        wait = 0;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //vel.y += forceAmt;      //Add upward force
            canSlow = true;
            value += forceAmt;
        }
        else
        {
            wait++;                 //Increase input checker 
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //vel.y -= forceAmt;      //Add downward force
            canSlow = true;
            value -= forceAmt;
        }
        else
        {
            wait++;                 //Increase input checker
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            angle -= angleAmt;      //Turn player right
        }
        else
        {
            wait++;                 //Increase input checker
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angle += angleAmt;      //Turn player left
        }
        else
        {
            wait++;                 //Increase input checker
        }

        //Set the rotation to the current angle if not hit
        if (velModifier != 0&&kb.playerAlive)
        {
            rb2d.constraints = RigidbodyConstraints2D.None;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        //Convert the given angle and force into a velocity vector
        vel.x = Mathf.Cos((angle)*Mathf.PI / 180) * value*.005f*velModifier;
        vel.y = Mathf.Sin((angle) * Mathf.PI / 180) * value*.005f*velModifier;

        //Clamp velocity as needed
        if (vel.magnitude > maxSpeed && velModifier != 2)
        {
            vel = Vector2.ClampMagnitude(vel, maxSpeed);
            print("clamped");
        }

        //Add velocity to the player
        if (!isCaught)
        {
            rb2d.AddForce(vel);
        }
        else
        {
            paw = GameObject.Find("Paw(Clone)");
            transform.position = paw.transform.position;
        }


    }

    /// <summary>
    /// Handles player decelleration. Runs every .1 second
    /// </summary>
    /// <returns></returns>
    IEnumerator LingeringVelocity()
    {
        //Sets how many times this function runs
        int timesRan = (int)(driftTime * 10);

        for (int i = 0; i < timesRan; i++)
        {
            //Get current velocity and multiply it by current slowdown rate
            vel = rb2d.velocity;
            vel *= slowdown;
            //Sets new velocity
            rb2d.velocity = vel;
            //Waits .1 second to run again
            yield return new WaitForSeconds(.1f);
        }

        //When slowdown is complete, stop the player
        rb2d.velocity = Vector2.zero;
        isWaiting = false;              //Decelleration completed
    }
    
    /// <summary>
    /// Ensures the player stays within the bounds of the game
    /// </summary>
    private void ClampPlayer()
    {
        //Reference to the current position
        clampedPos = transform.position;
        /*
        //A series of checks: if the player is out of bounds, put them in bounds
        if (transform.position.x > playableAreaWidth / 2)
        {
            clampedPos.x = playableAreaWidth / 2;
        }
        if (transform.position.x < -playableAreaWidth / 2)
        {
            clampedPos.x = -playableAreaWidth / 2;
        }
        if (transform.position.y > playableAreaHeight / 2)
        {
            clampedPos.y = playableAreaHeight / 2;
        }
        if (transform.position.y < -playableAreaHeight / 2)
        {
            clampedPos.y = -playableAreaHeight / 2;
        }*/

        //Set the player's position to the clamped position
        transform.position = clampedPos;
        if (kb.playerAlive)
        {
            positions.Add(clampedPos);
        }
    }


    #endregion

}
