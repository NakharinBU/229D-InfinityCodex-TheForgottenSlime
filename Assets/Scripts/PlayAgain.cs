using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    public GameObject playAgain;
    public void Died()
    {
        playAgain.SetActive(true);
        Time.timeScale = 0;
    }

    public void Home()
    {
        playAgain.SetActive(false);
        SceneManager.LoadScene("Mainmenu");
        Time.timeScale = 1;
    }

    public void Restart()
    {
        playAgain.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
