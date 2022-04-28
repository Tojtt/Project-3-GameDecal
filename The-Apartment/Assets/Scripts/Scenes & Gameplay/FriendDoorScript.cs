using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class FriendDoorScript : MonoBehaviour
{
    /*NOTES:
    1. Add collider to door
    2. Check isTrigger
    */

    /* For each door:
     * 1. List of dialogue for each day
     * 2. Random or ordered dialogue
     * 3. Empty or not on that day
     * 4. Event function
     */
    #region Editor_variables
    public DialogueRunner dialogueRunner;
    public string defaultDoorDialogueNode;
    public string coatDialogueNode;
    private GameState g;
    InMemoryVariableStorage varStorage;
    GameObject player;
    #endregion

    #region Non_editor_variables
    int clickCount = 0;
    DoorManager doorManager;
    #endregion

    #region Unity_functions
    void Awake()
    {
        doorManager = GameObject.Find("DoorManager").GetComponent<DoorManager>();
        g = GameObject.Find("GameManager").GetComponent<GameState>();
        varStorage = GameObject.Find("Dialogue System").GetComponent<InMemoryVariableStorage>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
       
    }
    #endregion


    #region Interact_functions
    public void Interact()
    {
        clickCount++;
        if (g.getCurrentTask().GetComponent<AbstractTask>().getTaskName() == "Return coat")
        {
            g.getCurrentTask().GetComponent<AbstractTask>().incrementProgress();
            dialogueRunner.Stop();
            dialogueRunner.StartDialogue(coatDialogueNode);
            //teleport player
        }
        else
        {
            dialogueRunner.StartDialogue(defaultDoorDialogueNode);
        }
    }

    void OnMouseDown()
    {
        Debug.Log("friend Door");
        if (doorManager.InClickRange(transform.position))
        {
            Interact();
        }

    }


    #endregion


    [YarnCommand("StartFriendRoomDialogue")]
    public void dialogue_teleport_from_door()
    {
        dialogueRunner.Stop();
        dialogueRunner.StartDialogue("FriendRoomDialogue");
    }


    //>>>>>LATER -> changing appearance/image: https://forum.unity.com/threads/changing-sprite-during-run-time.211817/
}
