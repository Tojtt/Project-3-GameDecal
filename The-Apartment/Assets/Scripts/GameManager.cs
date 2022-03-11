using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    #region Unity_functions

    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(this.gameObject);        
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Scene_transitions

        //Starting out game
    public void StartGame()
    {
        SceneManager.LoadScene("Hallway");
    }


    public void Hallway()
    {
        SceneManager.LoadScene("Hallway");
    }
    public void Apartment()
    {
        SceneManager.LoadScene("Apartment");
    }
    
    public void nightTransition()
    {
        SceneManager.LoadScene("NightCutScene");
    }
    #endregion


}
