using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnInteractable : MonoBehaviour
{
    #region Dialogue_vars
    private DialogueRunner dialogueRunner;
    public LineView lineview;
    #endregion 

    private bool canInteract;
    private bool isCurConvo;
    public int timesSpoken;

    public void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        lineview = FindObjectOfType<LineView>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
        canInteract = true;
        timesSpoken = 0;

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lineview.OnContinueClicked();
        }
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
            //dialogueRunner.StartDialogue(disableNode);
        }

        dialogueRunner.StartDialogue(conversationStartNode);
        timesSpoken++;
        Debug.Log("Hi");
    }

    private void EndConversation()
    {
        if (isCurConvo)
        {
            isCurConvo = false;
        }
        Debug.Log("Bye");
    }

    [YarnCommand("disable")]
    private void DisableConversation()
    {
        canInteract = false;
    }

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
