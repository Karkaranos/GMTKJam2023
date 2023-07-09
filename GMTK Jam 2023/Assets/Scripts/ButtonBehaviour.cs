using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject controlScreen;


    [SerializeField]
    private Text time;

    ScoreBehavior sb;

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void Start()
    {
        sb = GameObject.Find("ScoreHandler").GetComponent<ScoreBehavior>();
        if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 3)
        {
            time.text = "Time Survived: " + sb.minSurvived + ":" + sb.secSurvived;
        }
    }

    public void ControlsButton()
    {
        titleScreen.SetActive(false);
        controlScreen.SetActive(true);
    }

    public void BackButton()
    {
        titleScreen.SetActive(true);
        controlScreen.SetActive(false);
    }
    
}
