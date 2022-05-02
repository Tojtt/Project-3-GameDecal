using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class RitualDoorScript : MonoBehaviour
{

    //NOTE: this script provides exactly or almost exactly the same functionality as CobwebScript.
    //Maybe use abstraction if this coding pattern appears again in some other task!
    private GameObject task;
    public DialogueRunner dialogueRunner;

    void Start()
    {
        task = this.transform.parent.gameObject;
    }

    void OnMouseDown()
    {
        //task.GetComponent<RitualInvestigateTaskScript>().incrementProgress();
        Debug.Log("Ritual door clicked");
        StartCoroutine(task.GetComponent<RitualInvestigateTaskScript>().ScriptRitualCutscene());
    }
}
