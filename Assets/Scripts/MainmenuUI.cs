using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainmenuUI : MonoBehaviour
{
    public GameObject option;
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Option()
    {
        option.SetActive(true);
    }

    public void Back()
    { 
        option.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
