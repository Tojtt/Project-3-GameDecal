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
    // denotes whether the original dialogue is finished 
    public bool finished;
    // denotes whether the character should run a finished dilaogue
    public bool canFinish = false;

    public void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        lineview = FindObjectOfType<LineView>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
        canInteract = true;
        finished = false;
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

    // node that plays once, then disables talking to the character
    public string disableNode;

    // node that repeats after the main dialogue has ended 
    public string repeatNode;

    #region Yarn_Functions 
    public void StartConversation()
    {
        isCurConvo = true;
        if (canFinish && finished)
        {
            dialogueRunner.StartDialogue(repeatNode);
            return;
        }

        dialogueRunner.StartDialogue(conversationStartNode);
        finished = true;
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
