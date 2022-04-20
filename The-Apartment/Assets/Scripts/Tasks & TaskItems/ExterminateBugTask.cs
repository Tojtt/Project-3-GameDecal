using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ExterminateBugTask : AbstractTask
{
    #region Progress_Variables
    private bool isComplete; // denotes completion of task
    private string description; // denotes description of task
    #endregion

    #region Unity_Variables
    public DialogueRunner dialogueRunner;
    public string startNode = "sinkTaskStart";
    #endregion 

    #region AbstractTask_Functions
    public override void Awake()
    {
        //throw new System.NotImplementedException();
    }

    public override void Start()
    {
        isComplete = false;
        description = "Check your sink...";
        if (!dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.StartDialogue(startNode);
        }
    }

    public override void Update()
    {
        //throw new System.NotImplementedException();
    }

    public override string getProgessString()
    {
        throw new System.NotImplementedException();
    }

    public override string getTaskName()
    {
        throw new System.NotImplementedException();
    }

    public override void incrementProgress()
    {
        
    }

    public override bool isTaskComplete()
    {
        return isComplete;
    }

    #endregion 

}
