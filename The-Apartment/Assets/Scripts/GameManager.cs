using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public List<Item> itemList = new List<Item>();

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Inventory.instance.AddItem(this.itemList[Random.Range(0, itemList.Count)]);
        }
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
