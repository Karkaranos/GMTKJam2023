using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CageBehavior : MonoBehaviour
{
    GameController gc;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            if (gc.keyHeld)
            {
                StartCoroutine(GameWin());
            }
        }
    }
    
    IEnumerator GameWin()
    {
        gc.gameWon = true;
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);
    }

}
