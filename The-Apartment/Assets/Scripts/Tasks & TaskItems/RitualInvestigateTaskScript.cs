using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualInvestigateTaskScript : AbstractTask
{

    private bool doorClicked;

    public override void Awake()
    {
      
    }

    public override void Start()
    {
        doorClicked = false;
    }

    public override void Update()
    {
        
    }

    public override string getProgessString()
    {
        return "Sounds like its coming from downstairs...";
    }

    public override string getTaskName()
    {
        return "Find the source of the noise";
    }

    public override void incrementProgress()
    {
        doorClicked = true;
    }

    public override bool isTaskComplete()
    {
        return doorClicked;
    }
}
