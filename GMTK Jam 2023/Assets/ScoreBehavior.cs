using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBehavior : MonoBehaviour
{
    public int minSurvived;

    public int secSurvived;

    private void Awake()
    {
        int numMe = FindObjectsOfType<ScoreBehavior>().Length;
        if (numMe != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


}
