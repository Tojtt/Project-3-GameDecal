using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ExterminateBugTask : AbstractTask
{
    #region Progress_Variables
    private bool isComplete; // denotes completion of task
    private string description; // denotes description of task
    public bool isActive;
    private int spidersKilled;
    #endregion

    #region Minigame_Configs
    public int numSpiders = 10;
    public float speed = 5.0f;
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
        isActive = true;
        spidersKilled = 0;
        description = "Check your sink...";
        if (!dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.StartDialogue(startNode);
        }
    }

    public override void Update()
    {
        if (isComplete)
        {
            isActive = false;
        }
    }

    public override string getProgessString()
    {
        return "Spiders squashed: " + spidersKilled + "/" + numSpiders;
    }

    public override string getTaskName()
    {
        throw new System.NotImplementedException();
    }

    public override void incrementProgress()
    {
        // if spider killed
        spidersKilled += 1;

    }

    public override bool isTaskComplete()
    {
        return isComplete;
    }

    #endregion

    #region Cutscene_Functions
    public void SpawnSpiders()
    {
        
    }
    #endregion

}
