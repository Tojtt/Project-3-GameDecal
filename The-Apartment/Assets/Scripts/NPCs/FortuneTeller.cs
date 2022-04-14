using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class FortuneTeller : MonoBehaviour
{
    #region Dialogue Variables
    public string fortuneTellingDialogueNode;
    bool startedDialogue = false;
    #endregion

    #region Editor_Variables
    public DialogueRunner dialogueRunner;
    #endregion

    #region Referenced_Variables
    GameState gameState;
    GameObject player;
    InMemoryVariableStorage varStorage;
    #endregion

    void Awake()
    {
        gameState = GameObject.FindWithTag("GameManager").GetComponent<GameState>();
        player = GameObject.Find("Player");
        varStorage = GameObject.Find("Dialogue System").GetComponent<InMemoryVariableStorage>();
    }

    void Update()
    {
        if (gameState.inRoom && gameState.roomNum == 302) {
            if (!gameState.fortuneTellingComplete && !startedDialogue)
            {
                dialogueRunner.Stop(); //Stop any currently running dialogue
                startedDialogue = true;
                TellFortune();
            }
            if (startedDialogue)
            {
                CheckIfDialogueStopped();
            }
        } 
    }

    void TellFortune()
    {
        dialogueRunner.StartDialogue(fortuneTellingDialogueNode);
        
    }

    void CheckIfDialogueStopped()
    {
        float done = 0;
        varStorage.TryGetValue("$done", out done);
        // Debug.Log(done.ToString());
        if (done.ToString() == "2") //Dialogue Finished
        {
            Debug.Log("Fortune Telling complete");

            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.forcedTeleporter = GameObject.Find("HallwayDoor302").GetComponent<BoxCollider2D>().gameObject;
            playerController.ForcedTeleport();
            playerController.forcedTeleporter = null;
            
            Debug.Log("Should have teleported out");
            gameState.fortuneTellingComplete = true;
            startedDialogue = false;
        }
    }
}
