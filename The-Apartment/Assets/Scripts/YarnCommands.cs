using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnCommands : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        // Adds a Yarn Command to generate a number [0, range)
        GetComponent<DialogueRunner>().AddFunction("random",
            (int range) => { return Random.Range(0, range); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
