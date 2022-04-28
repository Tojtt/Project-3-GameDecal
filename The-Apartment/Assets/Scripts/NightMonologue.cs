using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NightMonologue : MonoBehaviour
{
    public DialogueRunner dialogue;
    public SceneTransitions st;

    public void Monologue()
    {
        int day = GameState.Instance.day;
        string startNode = "NightDialogue" + day;
        if (!dialogue.IsDialogueRunning)
        {
            dialogue.StartDialogue(startNode);
            dialogue.onDialogueComplete.AddListener(st.OnMouseDown);
        }
    }
}
