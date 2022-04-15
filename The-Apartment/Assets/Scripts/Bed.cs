using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Bed : MonoBehaviour
{
    #region Unity_variables
    public SceneTransitions sceneTransition;
    public DialogueRunner dialogue;
    public GameState gs;
    public string nextDay;
    #endregion

    public void Start()
    {
        dialogue = FindObjectOfType<DialogueRunner>();
        sceneTransition = FindObjectOfType<SceneTransitions>();
    }

    private void OnMouseDown()
    {
        Debug.Log("Bed clicked");
        Debug.Log(GameState.Instance.dayFinished);
        if (GameState.Instance.dayFinished)
        {
            if (GameState.Instance.watchedTV)
            {
                // can progress to next day
                //GameState.Instance.nextDay();
                GameState.Instance.day += 1;
                nextDay = "Day" + GameState.Instance.day + "Scene";
                Debug.Log(GameState.Instance.day);
                GameState.Instance.PrepareNextDay(GameState.Instance.day);
                GameState.Instance.watchedTV = false;
                StartCoroutine(sceneTransition.LoadScene(nextDay));
            } else
            {
                // run Dialogue telling Player to watch the TV
                runTVDialogue(0);
                
            }
        } else
        {
            runTVDialogue(1);
        }
    }

    public void runTVDialogue(int option)
    {
        if (!dialogue.IsDialogueRunning)
        {
            if (option == 0)
            {
                dialogue.StartDialogue("watchTV");
            } else if (option == 1)
            {
                dialogue.StartDialogue("finishChores");
            }
        }
    }
}
