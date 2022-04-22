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

    public ExterminateBugTask task;

    #endregion

    private void Start()
    {
        maincam = GameObject.Find("Main Camera");
        dialogue = FindObjectOfType<DialogueRunner>();
        task = GetComponent<ExterminateBugTask>();
    }

    private void OnMouseDown()
    {
        // do not trigger the cutscene if the task is not activated
        if (task == null || !task.isActive)
        {
            return;
        }

        // trigger the spiders cutscene
        Debug.Log("Sink clicked");

        // move camera to sink area, save current player position 
        Vector3 prevPos = player.transform.position;
        Vector3 target = sinkOverlay.transform.position;
        Debug.Log("Current position" + target);
        maincam.gameObject.GetComponent<CameraFollow>().followEnabled = false;
        maincam.transform.position = target;
        // disable player movement, start cutscene
        GameState.Instance.freezePlayer = true;
        Debug.Log("Moved player");
        if (!dialogue.IsDialogueRunning)
        {
            Debug.Log("Run sink dialogue");
            dialogue.StartDialogue("activateSink");
            // run cutscene
        }
        // snap back to position
        GameState.Instance.freezePlayer = false;

    }
}
