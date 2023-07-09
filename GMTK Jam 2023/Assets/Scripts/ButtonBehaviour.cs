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

    public int minSurvived;
    public int secSurvived;

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
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
