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
    public bool disabled;
    #endregion

    public void Start()
    {
        dialogue = FindObjectOfType<DialogueRunner>();
        sceneTransition = FindObjectOfType<SceneTransitions>();
        disabled = false;
    }

    private void OnMouseDown()
    {
        // don't allow user to skip day if disabled - for cutscenes involving the room
        if (disabled) return;

        if (GameState.Instance.dayFinished)
        {
            if (GameState.Instance.watchedTV)
            {
                // can progress to next day
                //GameState.Instance.nextDay();

                // special case for Day 2 to trigger Friend scene
                if (GameState.Instance.day == 2)
                {
                    GameState.Instance.RunFriendDinner();
                }

                GameState.Instance.day += 1;
                // special case for Day 4 and onward for entrance blocked
                if (GameState.Instance.day >= 4)
                {
                    GameState.Instance.entranceBlocked = true;
                }
                nextDay = "Day" + GameState.Instance.day + "Scene";
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
