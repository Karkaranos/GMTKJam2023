using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    Vector3 pos;

    // Update is called once per frame
    void Update()
    {
        pos = player.transform.position;
        pos.z = -10;
        transform.position = pos;
    }
}
