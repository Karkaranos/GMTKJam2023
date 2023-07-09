using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SauceBehavior : MonoBehaviour
{
    Renderer r;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
        StartCoroutine(FadeTimer());
    }

    IEnumerator FadeTimer()
    {
        yield return new WaitForSeconds(4);
        for(float i=1; i>0; i -= .05f)
        {
            r.material.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(.05f);

        }
        Destroy(gameObject);
    }
}
