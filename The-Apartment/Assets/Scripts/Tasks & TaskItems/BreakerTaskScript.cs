using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakerTaskScript : AbstractTask
{
    #region Progress Varaibles

    private bool isComplete;
    private string description;
    [SerializeField]
    private string taskName;
    private GameObject lights;

    #endregion

    // Start is called before the first frame update
    public override void Start()
    {
        isComplete = false;
        description = "The power has gone out, find the breaker!";
        taskName = "Power's out";

        // turn the lights out!
        lights = GameObject.Find("Lights");

        foreach (Transform child in lights.transform)
        {
            GameObject l = child.gameObject;
            l.GetComponent<LightScript>().turnOff();
        }

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
        return "Find and flip the power breaker downstairs";
    }

    public override void incrementProgress()
    {
        foreach (Transform child in lights.transform)
        {
            GameObject l = child.gameObject;
            l.GetComponent<LightScript>().turnOn();
        }
        isComplete = true;
    }

    public override bool isTaskComplete()
    {
        return isComplete;
    }
    #endregion
}
