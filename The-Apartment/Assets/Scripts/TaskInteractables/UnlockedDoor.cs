using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class UnlockedDoor : MonoBehaviour
{
    /*NOTES:
    1. Add collider to door
    2. Check isTrigger
    3.DISABLE PART 1 FORTUNE TELLER DOOR, AND FORTUNE TELLER GAME OBJECT
    */

    #region Editor_variables
    public DialogueRunner dialogueRunner;
    public string doorDialogueNode;
    #endregion

    #region Non_editor_variables
    int clickCount = 0;
    DoorManager doorManager;
    GameObject player;
    #endregion

    #region Unity_functions
    void Awake()
    {
        doorManager = GameObject.Find("DoorManager").GetComponent<DoorManager>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (doorManager.InClickRange(transform.position))
            {
                EnterRoom();
            }
        }

    }
    #endregion

    #region Interact_functions
    public void Interact()
    {
        clickCount++;
        dialogueRunner.Stop();
        dialogueRunner.StartDialogue(doorDialogueNode);
    }

    void OnMouseDown()
    {
        if (doorManager.InClickRange(transform.position))
        {
            Interact();
        }

    }

    void EnterRoom()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.forcedTeleporter = GetComponent<BoxCollider2D>().gameObject;
        playerController.ForcedTeleport();
        //StartCoroutine(playerController.ForcedfTeleport());
        playerController.forcedTeleporter = null;
        Debug.Log("Should have teleported");
    }
    #endregion

}