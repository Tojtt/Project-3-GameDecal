using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoatScript : MonoBehaviour
{
    GameObject task;
    // Start is called before the first frame update
    void Start()
    {
        task = this.transform.parent.gameObject;
    }

    void OnMouseDown()
    {
        task.GetComponent<ReturnClothingTaskScript>().incrementProgress();
        Destroy(this.transform.gameObject);
    }
}
