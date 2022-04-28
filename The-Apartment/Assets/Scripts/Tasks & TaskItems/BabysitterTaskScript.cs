using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class BabysitterTaskScript : AbstractTask
{

    #region Babysitter_task_state_vars
    bool accepted_cash;
    public DialogueRunner dialogueRunner;

    #endregion

    public override void Awake()
    {
        accepted_cash = false;
    }

    public override string getProgessString()
    {
        return "Find the mother on floor 1 and babysit her kids until she comes back.";
    }

    public override string getTaskName()
    {
        return "Babysitting";
    }

    public override void incrementProgress()
    {
        accepted_cash = true;
    }

    public override bool isTaskComplete()
    {
        return accepted_cash;
    }

    public override void Start()
    {
       
    }

    public override void Update()
    {
       
    }

    [YarnCommand("TriggerBabysitSequence")]
    public void babysitWrapper()
    {
        StartCoroutine(babysitSequence());
    }

    IEnumerator babysitSequence()
    {
        yield return new WaitForSeconds(2.5f);
        dialogueRunner.StartDialogue("EnterBabysitRoom");

    }

}
