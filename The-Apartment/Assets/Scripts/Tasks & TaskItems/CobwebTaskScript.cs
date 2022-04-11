using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobwebTaskScript : AbstractTask
{
    #region Progress Varaibles

    private bool isComplete;
    private string description;
    private int totalCobwebs;
    private int numCobwebsSwept;
    [SerializeField]
    private string taskName;

    #endregion

    // Start is called before the first frame update
    public override void Start()
    {

    }


    public override void Awake()
    {
        totalCobwebs = 3;
        isComplete = false;
        description = "Sweep up the cobwebs scattered around the building!";
        numCobwebsSwept = 0;
        taskName = "Cobwebs";
        DontDestroyOnLoad(gameObject);
    }

    public override void Update()
    {
       
    }

    #region Task Functions

    public override string getTaskName()
    {
        return taskName;
    }

    public override string getProgessString()
    {
        return numCobwebsSwept + " / " + totalCobwebs;
    }

    public override void incrementProgress()
    {
        numCobwebsSwept += 1;
        if (numCobwebsSwept == totalCobwebs)
        {
            isComplete = true;
        }
    }

    public override bool isTaskComplete()
    {
        return isComplete;
    }


    #endregion
}