using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobwebTaskScript : MonoBehaviour
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
    void Start()
    {
        totalCobwebs = 3;
        isComplete = false;
        description = "Sweep up the cobwebs scattered around the building!";
        numCobwebsSwept = 0;
        taskName = "Cobwebs";
    }

    #region Task Functions

    public string getTaskName()
    {
        return taskName;
    }

    public string getProgressString()
    {
        return numCobwebsSwept + " / " + totalCobwebs;
    }

    public void incrementProgress()
    {
        numCobwebsSwept += 1;
        if (numCobwebsSwept == totalCobwebs)
        {
            isComplete = true;
        }
    }



    #endregion
}