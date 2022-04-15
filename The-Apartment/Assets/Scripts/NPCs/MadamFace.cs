using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class MadamFace : MonoBehaviour
{

    /**TODO Later:
    1) Add door animation
    2) Disable after first closedShop dialogue
    3) Only start fortune telling dialogue when walk up to fortune teller. 
    4) Add pause before reaction.
    **/

    #region Appearance_Variables
    public Sprite closedDoorSprite;
    public Sprite openDoorSprite;
    SpriteRenderer spriteRenderer;
    #endregion

    #region Dialogue Variables
    public string crackOpenDialogueNode;
    public string reactionDialogueNode;
    public string knockDoorDialogueNode;
    public string closedShopDialogueNode;

    bool wasOutsideRange = true;
    #endregion

    #region Editor_Variables
    public DialogueRunner dialogueRunner;
    public float triggerDistance = 2.0f; //For door
    #endregion

    #region Game_Variables
    int roomNum = 302;
    int firstDay = 2;
    int emptyDay = 4;
    int lastDay = 6;

    public int doorTimes = 0;
    #endregion

    #region Referenced_Variables
    GameObject player;
    GameState gameState;
    InMemoryVariableStorage varStorage;
    #endregion
    
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        gameState = GameObject.FindWithTag("GameManager").GetComponent<GameState>();
        varStorage = GameObject.Find("Dialogue System").GetComponent<InMemoryVariableStorage>();
        //Set to closed door
        spriteRenderer.sprite = closedDoorSprite;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameState.day >= firstDay && gameState.day <= emptyDay && !gameState.fortuneTellingComplete) //Crack open door
        {
            if (doorTimes == 0)
            {
                CheckNearby();
            }

            if (gameState.freezePlayer)
            {
                TryToUnfreeze();
            }
        }
    }

    #region Behavior_Functions
    void CheckNearby()
    {
        if (gameState.floor == roomNum / 100)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < triggerDistance)
            {
                if (wasOutsideRange)//!gameState.inDialogue)
                {
                    OpenDoor(crackOpenDialogueNode);
                }
            }
            else
            {
                wasOutsideRange = true;
            }
        }
    }

    void OpenDoor(string dialogueNode)
    {
        dialogueRunner.Stop();
        Debug.Log("It's madam fortune teller!");
        wasOutsideRange = false;
        gameState.freezePlayer = true;
        //ADD ANIMATION HERE!!!!!!!!
        spriteRenderer.sprite = openDoorSprite;
        //Dialogue
        varStorage.SetValue("$done", 0);
        Debug.Log(dialogueNode);
        dialogueRunner.StartDialogue(dialogueNode);
        doorTimes++;
    }


    void TryToUnfreeze()
    {
        //float done = 0;
        //varStorage.TryGetValue("$done", out done);
        //// Debug.Log(done.ToString());
        string done = GetDialogueVariable("$done");
        if (done == "1") //Dialogue Finished
        {
            spriteRenderer.sprite = closedDoorSprite;
            Debug.Log("stopped");
            //if (doorTimes == 1)
            //{
            //    PlayerController playerController = player.GetComponent<PlayerController>();
            //    playerController.forcedTeleporter = GetComponent<BoxCollider2D>().gameObject;
            //    playerController.ForcedTeleport();
            //    playerController.forcedTeleporter = null;
            //    Debug.Log("Should have teleported");
            //}
            

            //Determine whether teleport in room, based on response (yes/no) to if want fortune told (0 = No)
            string response = GetDialogueVariable("$response");
            if (response == "1") //Go inside
            {
                PlayerController playerController = player.GetComponent<PlayerController>();
                playerController.forcedTeleporter = GetComponent<BoxCollider2D>().gameObject;
                playerController.ForcedTeleport();
                playerController.forcedTeleporter = null;
                Debug.Log("Should have teleported");
            }
            else
            {
                if (doorTimes == 1)
                {
                    dialogueRunner.StartDialogue(reactionDialogueNode);
                }
                //Close Door
                
            }

            gameState.freezePlayer = false;
        }
        
    }

    void OnMouseDown()
    {
        if (doorTimes > 0 && !gameState.freezePlayer) //Knocking while fortune teller is home
        {
            if (!gameState.fortuneTellingComplete) //Fortune not told yet
            {
                OpenDoor(knockDoorDialogueNode);
            } else                                 //Closed
            {
                dialogueRunner.StartDialogue(closedShopDialogueNode);
            }
        } else if (gameState.day >= emptyDay && gameState.day <= lastDay) //The place is empty, can go in
        {
            //Teleport in
            //Escape room task
        }
        
    }

    //Gets variables from dialogue
    string GetDialogueVariable(string var)
    {
        float value = 0;
        varStorage.TryGetValue(var, out value);
        return value.ToString();
    }
    #endregion



}
