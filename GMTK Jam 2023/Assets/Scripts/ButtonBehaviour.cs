using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject controlScreen;

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
