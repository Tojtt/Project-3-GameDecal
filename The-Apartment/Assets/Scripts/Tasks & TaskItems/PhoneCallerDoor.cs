using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PhoneCallerDoor : MonoBehaviour
{
    /*NOTES:
    1. Add collider to door
    2. Check isTrigger
    */

    #region Editor_variables
    public DialogueRunner dialogueRunner;
    public string todaysDialogueNode;
    public string randomDialogueNode;
    #endregion

    #region Non_editor_variables
    int clickCount = 0;
    DoorManager doorManager;
    GameState gameState;

    bool heardTodaysCall = false; //After hearing the main phone call, random other dialogue
    #endregion

    #region Unity_functions
    void Awake()
    {
        gameState = GameObject.FindWithTag("GameManager").GetComponent<GameState>();
        doorManager = GameObject.Find("DoorManager").GetComponent<DoorManager>();
    }
    #endregion

    #region Interact_functions
    public void Interact()
    {
        clickCount++;
        if (!heardTodaysCall)
        {
            dialogueRunner.StartDialogue(todaysDialogueNode);
            heardTodaysCall = true;
        } else
        {
            dialogueRunner.StartDialogue(randomDialogueNode);
        }
        
        
    }

    void OnMouseDown()
    {
        if (doorManager.InClickRange(transform.position))
        {
            Interact();
        }

    }


    #endregion
}
