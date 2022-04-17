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
        Debug.Log("Dinner item clicked");
      
        MakeDinnerTask dinnerTask = task.GetComponent<MakeDinnerTask>();
        Debug.Log(dinnerTask.stage + " " + this.stage);
        if (dinnerTask.stage == this.stage)
        {
            dinnerTask.incrementProgress();
            Destroy(this.transform.gameObject);
        }
    }
}
