using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualDoorScript : MonoBehaviour
{

    //NOTE: this script provides exactly or almost exactly the same functionality as CobwebScript.
    //Maybe use abstraction if this coding pattern appears again in some other task!
    private GameObject task;

    void Start()
    {
        task = this.transform.parent.gameObject;
    }

    void OnMouseDown()
    {
        task.GetComponent<RitualInvestigateTaskScript>().incrementProgress();
    }
}
