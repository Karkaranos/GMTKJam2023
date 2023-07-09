using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvocadoPitBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject knife;
    private KnifeBehavior kb;
    Rigidbody2D rb2d;

    public bool isFree=false;
    public bool isOnKnife = false;
    public bool isWithPlayer=false;
    // Start is called before the first frame update
    void Start()
    {
        kb = knife.GetComponent<KnifeBehavior>();
        rb2d=GetComponent<Rigidbody2D>();
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Knife" && collision.gameObject.GetComponent<KnifeBehavior>().isFallen && isFree == false && isOnKnife == false)
        {
            isFree = true;
            isOnKnife = true;
            print("hit");
            StartCoroutine(FollowKnife());
        }
    }

    IEnumerator FollowKnife()
    {
        for (float i = 4; i < 5; i+=.1f)
        {
            yield return new WaitForSeconds(.1f);
            transform.localScale = new Vector3(i, i, i);
        }
        Vector3 followPos;
        rb2d.constraints = RigidbodyConstraints2D.None;
        while (isOnKnife)
        {
            followPos = knife.transform.position;
            followPos.y += 2;
            transform.position = followPos;
            if (kb.isAttacking == false)
            {
                isOnKnife = false;
                for (float i = 5f; i > 4; i-=.1f)
                {
                    yield return new WaitForSeconds(.1f);
                    transform.localScale = new Vector3(i, i, i);
                }
            }
        }
    }
}
