using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTransition : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Day1Scene");
    }

    public void EndGameWin()
    {
        SceneManager.LoadScene("GameWin");
    }

    public void EndGameLose()
    {
        SceneManager.LoadScene("GameLose");

    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Scene0");
    }
}
