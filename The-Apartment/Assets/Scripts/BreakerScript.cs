using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakerScript : MonoBehaviour
{
    // Start is called before the first frame update
    BreakerTaskScript task;
    void Start()
    {
        task = this.transform.parent.gameObject.GetComponent<BreakerTaskScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        task.incrementProgress();
    }
}
