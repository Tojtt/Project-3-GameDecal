using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskInfoScript : MonoBehaviour
{
    GameState gs;
    GameManager gm;
    UnityEngine.UI.Text tx;
    // Start is called before the first frame update
    void Start()
    {
        gs = gameObject.transform.parent.parent.GetComponent<GameState>();
        gm = gameObject.transform.parent.parent.GetComponent<GameManager>();
        tx = gameObject.GetComponent<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject task = gs.getCurrentTask();
        if (task != null)
        {
            // use inheritance to make this more general later!
            AbstractTask s = task.GetComponent<AbstractTask>();
            tx.text = "Current task: " + s.getTaskName() + ", Progress: " + s.getProgessString();
        }
        else
        {
            tx.text = "All done! Feel free to sleep :)";

        }
    }
}
