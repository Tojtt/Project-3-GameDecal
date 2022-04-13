using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{
    #region Unity_variables
    public SceneTransitions sceneTransition;
    public GameState gs;
    public PlayerController player;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
        sceneTransition = FindObjectOfType<SceneTransitions>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        // Load TV scene
        Debug.Log("TV Clicked");
        gs.watchedTV = true;
        player.DoTeleport();

        // run TV cutscene here?
    }
}
