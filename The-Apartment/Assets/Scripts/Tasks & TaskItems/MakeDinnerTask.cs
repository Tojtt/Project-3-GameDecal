using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDinnerTask : AbstractTask
{
    #region Progress_Variables
    private bool isComplete;
    private string description;
    private string stageDesc;
    private int totalStages;
    public int stage;
    [SerializeField]
    private string taskName;
    #endregion

    #region AbstractTask_Funcs
    // Start is called before the first frame update
    public override void Start()
    {
        isComplete = false;
        stage = 0;
        description = "Make dinner for your friend!\n" + getStageDesc(stage);
        totalStages = 4;
        taskName = "Dinner";

    }

    public override string getProgessString()
    {
        return getStageDesc(stage);
    }

    public override void incrementProgress()
    {
        stage += 1;
        description = "Make dinner for your friend!\n" + getStageDesc(stage);
        if (stage == totalStages)
        {
            isComplete = true; 
        }
    }

    public override void Update()
    {
        
    }

    public override string getTaskName()
    {
        return taskName;
    }

    public override bool isTaskComplete()
    {
        return isComplete;
    }

    #endregion

    private string getStageDesc(int stage)
    {
        switch (stage)
        {
            case 0:
                // code block
                return "Get the pizza dough on the counter";
            case 1:
                // code block
                return "Add in the tomato sauce";
            case 2:
                return "Add in cheese from the cabinet";
            case 3:
                return "Put the pizza in the oven";
            default:
                // code block
                return "Error: Stage not found";
        }
    }

    
}
