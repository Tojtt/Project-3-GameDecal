using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class RitualInvestigateTaskScript : AbstractTask
{

    private bool taskFinished;
    public DialogueRunner dialogueRunner;

    public override void Awake()
    {
      
    }

    public override void Start()
    {
        taskFinished = false;
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
        taskFinished = true;
    }

    public override bool isTaskComplete()
    {
        return taskFinished;
    }

    public IEnumerator ScriptRitualCutscene()
    {
        //Teleport the player to the ritual room
        //Trigger dialogue
        //Trigger sound effect; horror + chanting
        //Trigger orient all models toward player
        //make them start walking towards player
        //dialogue "im getting out of here!!!"
        //walk away
        //teleport away, trigger endgame (day6)
        Debug.Log("In Coroutine");
        dialogueRunner.StartDialogue("RitualApproachDoor");
        yield return new WaitForSeconds(0.1f);
    }

    [YarnCommand("inRitualDialogue")]
    public void in_ritual_dialogue_door()
    {
        dialogueRunner.Stop();
        dialogueRunner.StartDialogue("InRitual");
    }

    [YarnCommand("RitualTurnAndLeave")]
    public void ritual_anim()
    {
        //Trigger orient all models toward player
        //make them start walking towards player
        //dialogue "im getting out of here!!!"
        //walk away
        //teleport away, trigger endgame (day6)
        dialogueRunner.Stop();
        dialogueRunner.StartDialogue("RitualLeave");
    }

}
