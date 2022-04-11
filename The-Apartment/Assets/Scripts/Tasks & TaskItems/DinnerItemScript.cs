using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinnerItemScript : MonoBehaviour
{

    private GameObject task;
    public int stage;
    private Vector3 offset;

    void Start()
    {
        task = this.transform.parent.gameObject;
    }

    void OnMouseDown()
    {

        MakeDinnerTask dinnerTask = task.GetComponent<MakeDinnerTask>();
        if (dinnerTask.stage == this.stage)
        {
            dinnerTask.incrementProgress();
            Destroy(this.transform.gameObject);
        }
    }
}