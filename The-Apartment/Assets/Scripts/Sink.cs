using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Sink : MonoBehaviour
{
    #region Unity_Variables
    // camera
    public GameObject maincam;
    // dialogue runner
    public DialogueRunner dialogue;
    // sink overlay object
    public GameObject sinkOverlay;
    // player
    public GameObject player;
    GameState gameState;
    ExterminateBugTask task;

    #endregion

    private void Start()
    {
        maincam = GameObject.Find("Main Camera");
        dialogue = FindObjectOfType<DialogueRunner>();
        player = GameObject.Find("Player");
        task = sinkOverlay.GetComponent<ExterminateBugTask>();
        gameState = GameObject.FindWithTag("GameManager").GetComponent<GameState>();

    }

    private void OnMouseDown()
    {
        // do not trigger the cutscene if the task is not activated
        /* if (task == null || !task.isActive)
        {
            return;
        } */

        // trigger the spiders cutscene
        Debug.Log("Sink clicked");

        // move camera to sink area, save current player position 
        Vector3 prevPos = player.transform.position;
        Vector3 target = sinkOverlay.transform.position;
        Debug.Log("Current position" + target);
        maincam.gameObject.GetComponent<CameraFollow>().followEnabled = false;
        maincam.transform.position = new Vector3(target.x, target.y, -1);
        // disable player movement, start cutscene
        gameState.freezePlayer = true;
        Debug.Log("Moved player");
        if (!dialogue.IsDialogueRunning)
        {
            Debug.Log("Run sink dialogue");
            dialogue.StartDialogue("activateSink");
            
            // run cutscene
            //GetComponent<ExterminateBugTask>().SpawnSpiders();
        }
        task.Spawn();
        // stall until the task is completed?
        // while (!task.isTaskComplete()) { }

        // snap back to position
        gameState.freezePlayer = false;

    }
}
