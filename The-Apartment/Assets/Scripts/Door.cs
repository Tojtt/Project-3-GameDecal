using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Door : MonoBehaviour
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
    public string doorDialogueNode;
    #endregion

    #region Non_editor_variables
    int clickCount = 0;
    DoorManager doorManager;
    #endregion

    #region Unity_functions
    void Awake()
    {
        doorManager = GameObject.Find("DoorManager").GetComponent<DoorManager>();
    }
    #endregion

    #region Interact_functions
    public void Interact()
    {
        clickCount++;
        dialogueRunner.StartDialogue(doorDialogueNode);
    }

    void OnMouseDown()
    {
        if (doorManager.InClickRange(transform.position))
        {
            
            Interact();
        }
        
    }
    
    
    #endregion

    
    //>>>>>LATER -> changing appearance/image: https://forum.unity.com/threads/changing-sprite-during-run-time.211817/
}
