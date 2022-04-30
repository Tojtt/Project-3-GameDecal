using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Bed : MonoBehaviour
{
    #region Unity_variables
    public SceneTransitions sceneTransition;
    public GameObject doorExit;
    public DialogueRunner dialogue;
    public GameObject nightScene;
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
                    //GameState.Instance.RunFriendDinner();
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
                // run night monologue first
                int day = GameState.Instance.day - 1;
                string startNode = "NightDialogue" + day;
                if (!dialogue.IsDialogueRunning)
                {
                    doorExit.SetActive(false);
                    dialogue.StartDialogue(startNode);
                    // update camera to show the player sleeping
                    GameObject player = GameObject.Find("Player");
                    GameObject maincam = GameObject.Find("Main Camera");
                    Vector3 prevPos = player.transform.position;
                    Vector3 target = nightScene.transform.position;
                    Debug.Log("Current position" + target);
                    maincam.gameObject.GetComponent<CameraFollow>().followEnabled = false;

                    maincam.transform.position = target;
                    GameState.Instance.freezePlayer = true;
                    

                    dialogue.onDialogueComplete.AddListener(RunLoadScene);
                }
                
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

    public void RunLoadScene()
    {
        GameState.Instance.freezePlayer = false;
        doorExit.SetActive(true);
        StartCoroutine(sceneTransition.LoadScene(nextDay));
    }
}
