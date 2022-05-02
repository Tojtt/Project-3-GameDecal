using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Bathroom : MonoBehaviour { 

    public DialogueRunner dialogueRunner;
    public string dialogueNode;
    void OnMouseDown()
    {
        dialogueRunner.Stop();
        dialogueRunner.StartDialogue(dialogueNode);
    }
}
