using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class UnlockDoor : MonoBehaviour
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
    public int keyID;
    #endregion

    #region Non_editor_variables
    int clickCount = 0;
    DoorManager doorManager;
    GameObject player;
    InventoryManager invM;
    bool unlocked = false;
    #endregion

    #region Unity_functions
    void Awake()
    {
        invM = GameObject.FindWithTag("Inventory").GetComponent<InventoryManager>();

        doorManager = GameObject.Find("DoorManager").GetComponent<DoorManager>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (doorManager.InClickRange(transform.position))
            {
                if (unlocked) {
                    EnterRoom();
                }
            }
        }
        
    }
    #endregion

    #region Interact_functions
    public void Interact()
    {
        if (!unlocked)
        {
            if (invM.GetSelectedID() == keyID)
            {
                UnlockTheDoor();
            }
            else
            {
                clickCount++;
                dialogueRunner.Stop();
                dialogueRunner.StartDialogue(doorDialogueNode);
            }
                
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Door");
        if (doorManager.InClickRange(transform.position))
        {
            Interact();
        }

    }

    void UnlockTheDoor()
    {
        unlocked = true;
        //ADD IN SLIGHTLY AJAR DOOR SPRITE
        Debug.Log("Unlocked door");
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
