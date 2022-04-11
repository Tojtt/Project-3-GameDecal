using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class StaringNPC : MonoBehaviour
{

    #region Dialogue_variables
    private DialogueRunner dialogueRunner;

    private bool canInteract;

    private bool isCurConvo;

    public int timesSpoken;
    #endregion

    #region Appearance_variables
    public Sprite stareLeft;
    public Sprite stareCenter;
    public Sprite stareRight;
    public int stare; //0 = Left, 1 = Center, 2 = Right
    double halfRange = 0.5;
    #endregion
    GameObject player;

    #region Unity_functions
    public void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
        canInteract = true;
        timesSpoken = 0;
        this.GetComponent<SpriteRenderer>().sprite = stareLeft; //Stares left
        stare = 0; //Left
        player = GameObject.Find("Player");
    }

    public void Update()
    {
        double playerX = player.transform.position.x;
        double x = transform.position.x;
        if (playerX < x - halfRange) //Left
        {
            if (stare != 0) { 
            this.GetComponent<SpriteRenderer>().sprite = stareLeft;
            stare = 0;
            }
        } else if (playerX > x + halfRange) //Right
        {
            if (stare != 2) {
                this.GetComponent<SpriteRenderer>().sprite = stareRight;
                stare = 2;
            }
        } else if (stare != 1) //Center
        {
            this.GetComponent<SpriteRenderer>().sprite = stareCenter;
            stare = 1;
        }
    }
    #endregion

    // then we need a function to tell Yarn Spinner to start from {specifiedNodeName}
    public string conversationStartNode;

    public string disableNode;

    #region Yarn_Functions 
    private void StartConversation()
    {
        isCurConvo = true;
        // small test in disabling character dialogue
        if (timesSpoken == 2)
        {
            dialogueRunner.StartDialogue(disableNode);
        }

        dialogueRunner.StartDialogue(conversationStartNode);
        //timesSpoken++;
    }

    private void EndConversation()
    {
        if (isCurConvo)
        {
            isCurConvo = false;
        }

    }

    //[YarnCommand("disable")]
    //private void DisableConversation()
    //{
    //    canInteract = false;
    //}

    #endregion

    #region Interact_Functions 
    public void OnMouseDown()
    {
        if (canInteract && !dialogueRunner.IsDialogueRunning)
        {
            StartConversation();
        }
    }
    #endregion
}
