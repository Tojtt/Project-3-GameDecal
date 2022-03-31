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
    public bool random;
    public int dialogue_id;

    public DialogueRunner dialogueRunner;

    public string doorDialogueNode;

    public string dialogue;
    #endregion

    #region Non_editor_variables
    int clickCount = 0;
    DoorManager doorManager;
    #endregion

    #region Unity_functions
    void Awake()
    {
        doorManager = GameObject.Find("DoorManager").GetComponent<DoorManager>();
        //dialogueRunner.AddCommandHandler("random", GetDialogue);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Interact_functions
    public void Interact()
    {
        clickCount++;
        //Replace with interact code
        Debug.Log(name + " clicked");
        dialogue = doorManager.GetDialogue(dialogue_id, clickCount);
        Debug.Log(dialogue);
        dialogueRunner.StartDialogue(doorDialogueNode);
    }

    [YarnCommand("GetDoorDialogue")]
    public string GetDialogue()
    {
        Debug.Log("Gets door dialogue");
        return dialogue;
    }

    void OnMouseDown()
    {
        //LATER: add wait timer for in between clicks
        if (doorManager.InClickRange(transform.position))
        {
            
            Interact();
        }
        
    }
    
    
    #endregion

    
    //>>>>>LATER -> changing appearance/image: https://forum.unity.com/threads/changing-sprite-during-run-time.211817/
}
