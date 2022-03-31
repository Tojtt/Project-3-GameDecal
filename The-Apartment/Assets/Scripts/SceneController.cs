using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("A place to keep the default player object in the level. If a player object already exists, delete this one.")]
    private GameObject m_Player;
    
    static GameObject p_PlayerInstance;
	#endregion

	#region Private Variables
    private string p_SceneName;
	#endregion

	#region Initialization Methods
	private void Awake()
    {
        DontDestroyOnLoad(m_Player);
        if (p_PlayerInstance == null)
        {
            p_PlayerInstance = m_Player; 
        
        } else {
            Destroy(m_Player);
        }

        if (SceneManager.GetActiveScene().name.Equals("Menu"))
        {
            p_PlayerInstance.GetComponent<Transform>().position = new Vector2(100,100);
            p_PlayerInstance.GetComponent<Rigidbody2D>().gravityScale = 1;
        } else {
            
            p_PlayerInstance.GetComponent<Transform>().position = new Vector2(0,-1.5f);
            p_PlayerInstance.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
    
	private void Start ()
    {
        m_Player = GameObject.Find("Player");
        //GetActiveScene() returns a scene object
        p_SceneName = SceneManager.GetActiveScene().name;
	}
    #endregion

    #region Main Updates
    private void Update ()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
	}

    private void ReloadScene()
    {
        SceneManager.LoadSceneAsync(p_SceneName);
    }

    #endregion
}
