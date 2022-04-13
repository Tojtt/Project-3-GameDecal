using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class MadamFace : MonoBehaviour
{
    #region Dialogue Variables
    public string crackOpenDialogueNode;
    public string reactionDialogueNode;
    public string knockDoorDialogueNode;
    bool wasOutsideRange = true;
    #endregion

    #region Editor_Variables
    public DialogueRunner dialogueRunner;
    public float triggerDistance = 2.0f; //For door
    #endregion

    #region Game_Variables
    int roomNum = 202; //Change to 302 later
    int firstDay = 2;
    int lastDay = 6;

    public int doorTimes = 0;
    #endregion

    #region Referenced_Variables
    GameObject player;
    GameState gameState;
    InMemoryVariableStorage varStorage;// = GameObject.Find("Dialogue System").GetComponent<InMemoryVariableStorage>();
    #endregion
    
    void Start()
    {
        player = GameObject.Find("Player");
        gameState = GameObject.FindWithTag("GameManager").GetComponent<GameState>();
        varStorage = GameObject.Find("Dialogue System").GetComponent<InMemoryVariableStorage>();
        //Set to closed door
        //this.gameObject.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameState.day >= firstDay && gameState.day <= lastDay && !gameState.fortuneTellingComplete) //Crack open door
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
        Debug.Log("It's madam fortune teller!");
        wasOutsideRange = false;
        gameState.freezePlayer = true;
        //ADD ANIMATION HERE!!!!!!!!

        //Dialogue
        varStorage.SetValue("$done", 0);
        Debug.Log(dialogueNode);
        dialogueRunner.StartDialogue(dialogueNode);
        doorTimes++;
    }


    void TryToUnfreeze()
    {
        float done = 0;
        varStorage.TryGetValue("$done", out done);
        if (done != null)
        {
           // Debug.Log(done.ToString());
            if (done.ToString() == "1")
            {
                Debug.Log("stopped");
                gameState.freezePlayer = false;
            }
        }
        
    }

    void OnMouseDown()
    {
        if (doorTimes > 0 && !gameState.freezePlayer)
        {
            OpenDoor(knockDoorDialogueNode);
        }
        
    }

    #endregion



}
