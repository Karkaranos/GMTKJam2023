/*****************************************************************************
// File Name :         CameraBehavior.cs
// Author :            Cade R. Naylor
// Creation Date :     July 7, 2023
//
// Brief Description : Sets the camera to follow the player
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    Vector3 pos;

    /// <summary>
    /// Update is called every frame. Sets camera position to the player's position
    /// </summary>
    void Update()
    {
        pos = player.transform.position;
        pos.z = -10;                            //Adjustment so game is visible
        transform.position = pos;
    }
}
