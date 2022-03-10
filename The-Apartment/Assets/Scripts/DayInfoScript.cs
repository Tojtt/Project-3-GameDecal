using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayInfoScript : MonoBehaviour
{
    GameState gs;
    UnityEngine.UI.Text tx;
    // Start is called before the first frame update
    void Start()
    {
        gs = gameObject.transform.parent.parent.GetComponent<GameState>();
        tx = gameObject.GetComponent<UnityEngine.UI.Text>();
        tx.text = "Day: " + gs.day;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
