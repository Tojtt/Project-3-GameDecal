using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskInfoScript : MonoBehaviour
{
    GameState gs;
    UnityEngine.UI.Text tx;
    // Start is called before the first frame update
    void Start()
    {
        gs = gameObject.transform.parent.parent.GetComponent<GameState>();
        tx = gameObject.GetComponent<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gs);
        GameObject task = gs.getCurrentTask();
        if (task != null)
        {
            // use inheritance to make this more general later!
            CobwebTaskScript s = task.GetComponent<CobwebTaskScript>();
            Debug.Log(s);
            tx.text = "Current task: " + s.getTaskName() + ", Progress: " + s.getProgressString();
        }
        else
        {
            tx.text = "All done! Feel free to sleep :)";
        }
    }
}
