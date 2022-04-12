using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Bed : MonoBehaviour
{
    #region Unity_variables
    public SceneTransitions sceneTransition;
    public DialogueRunner dialogue;
    #endregion

    public void Start()
    {
        dialogue = FindObjectOfType<DialogueRunner>();
        sceneTransition = FindObjectOfType<SceneTransitions>();

    }

    private void OnMouseDown()
    {
        Debug.Log("Bed clicked");
        //Debug.Log(GameState.Instance.dayFinished);
        if (GameState.Instance.dayFinished)
        {
            if (GameState.Instance.watchedTV)
            {
                // can progress to next day
                GameState.Instance.nextDay();
            } else
            {
                // run Dialogue telling Player to watch the TV
                runTVDialogue();
                
            }
        } else
        {
            runTVDialogue();
        }
    }

    public void runTVDialogue()
    {
        if (!dialogue.IsDialogueRunning)
        {
            dialogue.StartDialogue("watchTV");
        }
    }
}
