using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnInteractable : MonoBehaviour
{
    private DialogueRunner dialogueRunner;

    private bool canInteract;

    private bool isCurConvo;

    public int timesSpoken;

    public void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
        canInteract = true;
        timesSpoken = 0;

    }

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
        timesSpoken++;
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
