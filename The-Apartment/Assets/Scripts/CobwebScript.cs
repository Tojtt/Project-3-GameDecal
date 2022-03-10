using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobwebScript : MonoBehaviour
{
    private GameObject task;

    void Start()
    {
        task = this.transform.parent.gameObject;
    }

    void OnMouseDown()
    {
        task.GetComponent<CobwebTaskScript>().incrementProgress();
        Destroy(this.transform.gameObject);
    }
}
