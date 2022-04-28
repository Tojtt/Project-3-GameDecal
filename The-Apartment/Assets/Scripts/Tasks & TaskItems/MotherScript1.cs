using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherScript1 : MonoBehaviour
{

    private GameObject task;

    void Start()
    {
        task = this.transform.parent.gameObject;
    }

    void OnMouseDown()
    {
        task.GetComponent<BabysitterTaskScript>().incrementProgress();
    }
}
