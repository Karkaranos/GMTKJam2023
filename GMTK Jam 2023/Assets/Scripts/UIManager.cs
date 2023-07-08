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

    PlayerCollisionBehavior pcb;

    // Start is called before the first frame update
    void Start()
    {
        pcb = GameObject.Find("Tomato").GetComponent<PlayerCollisionBehavior>();
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
        }
        if (pcb.lives == 1)
        {
            var img = heart1.GetComponent<Image>();
            img.sprite = fullHeart;
            img = heart2.GetComponent<Image>();
            img.sprite = splatHeart;
            img = heart3.GetComponent<Image>();
            img.sprite = splatHeart;
        }
        if (pcb.lives == 0)
        {
            var img = heart1.GetComponent<Image>();
            img.sprite = splatHeart;
            img = heart2.GetComponent<Image>();
            img.sprite = splatHeart;
            img = heart3.GetComponent<Image>();
            img.sprite = splatHeart;
        }
    }
}