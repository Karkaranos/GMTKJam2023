/*****************************************************************************
// File Name :         UIManager.cs
// Author :            Cade R. Naylor
// Creation Date :     July 7, 2023
//
// Brief Description : Controls game's UI
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Image heart1;
    [SerializeField]
    private Image heart2;
    [SerializeField]
    private Image heart3;

    [SerializeField]
    private Sprite fullHeart;
    [SerializeField]
    private Sprite splatHeart;
    [SerializeField]
    private Text timeText;

    private int secSurvived;
    private int minSurvived;

    PlayerCollisionBehavior pcb;

    // Start is called before the first frame update
    void Start()
    {
        pcb = GameObject.Find("Tomato").GetComponent<PlayerCollisionBehavior>();
        StartCoroutine(Timer());

    }

    // Update is called once per frame
    void Update()
    {
        Lives();
    }

    private void Lives()
    {
        if (pcb.lives == 3)
        {
            var img = heart1.GetComponent<Image>();
            img.sprite = fullHeart;
            img = heart2.GetComponent<Image>();
            img.sprite = fullHeart;
            img = heart3.GetComponent<Image>();
            img.sprite = fullHeart;
        }
        if (pcb.lives == 2)
        {
            var img = heart1.GetComponent<Image>();
            img.sprite = fullHeart;
            img = heart2.GetComponent<Image>();
            img.sprite = fullHeart;
            img = heart3.GetComponent<Image>();
            img.sprite = splatHeart;
            img.transform.localScale = new Vector3(.6f, .6f, .6f);
        }
        if (pcb.lives == 1)
        {
            var img = heart1.GetComponent<Image>();
            img.sprite = fullHeart;
            img = heart2.GetComponent<Image>();
            img.sprite = splatHeart;
            img.transform.localScale = new Vector3(.6f, .6f, .6f);
            img = heart3.GetComponent<Image>();
            img.sprite = splatHeart;
        }
        if (pcb.lives == 0)
        {
            var img = heart1.GetComponent<Image>();
            img.sprite = splatHeart;
            img.transform.localScale = new Vector3(.6f, .6f, .6f);
            img = heart2.GetComponent<Image>();
            img.sprite = splatHeart;
            img = heart3.GetComponent<Image>();
            img.sprite = splatHeart;
        }
    }

    IEnumerator Timer()
    {
        while (pcb.lives > 0)
        {
            yield return new WaitForSeconds(1f);
            secSurvived++;
            if (secSurvived == 60)
            {
                minSurvived++;
                secSurvived = 0;
            }
            if (secSurvived < 10)
            {
                timeText.text = ("Time Survived: " + minSurvived + ":0" + secSurvived);
            }
            else
            {
                timeText.text = ("Time Survived: " + minSurvived + ":" + secSurvived);
            }
        }
    }
}
