using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TV : MonoBehaviour
{
    #region Unity_variables
    public SceneTransitions sceneTransition;
    public GameState gs;
    public PlayerController player;
    public DialogueRunner dialogue;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
        sceneTransition = FindObjectOfType<SceneTransitions>();
        dialogue = FindObjectOfType<DialogueRunner>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        // Load TV scene
        Debug.Log("TV Clicked");
        GameState.Instance.watchedTV = true;
        if (!dialogue.IsDialogueRunning)
        {
            dialogue.StartDialogue("TVDay0");
        }

        //player.DoTeleport();

        // run TV cutscene here?
    }
}
