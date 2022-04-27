using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("A place to keep the default player object in the level. If a player object already exists, delete this one.")]
    private GameObject m_Player;
    
    static GameObject p_PlayerInstance;

    public static GameManager Instance = null;
	#endregion


    #region Awake_functions

    private void Awake() 
    {
        //DontDestroyOnLoad(m_Player);
        if (p_PlayerInstance == null)
        {
            p_PlayerInstance = m_Player; 
        
        } else {
            Destroy(m_Player);
        }

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Scene_transitions

        //Starting out game
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
