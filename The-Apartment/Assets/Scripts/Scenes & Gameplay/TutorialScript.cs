using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class TutorialScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject playerChar;
    public GameObject friendChar;
    private DialogueRunner dialogueRunner;
    public LineView lineview;
    private int tutorialProgress;
    [SerializeField]
    public List<String> tutorialNodes;

    void Start()
    {
        playerChar = GameObject.Find("Player");
        friendChar = GameObject.Find("Friend");

        dialogueRunner = FindObjectOfType<DialogueRunner>();
        lineview = FindObjectOfType<LineView>();

        tutorialProgress = 0;
        dialogueRunner.onDialogueComplete.AddListener(ProgressTutorial);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ProgressTutorial ()
    {
        dialogueRunner.Stop();
        if (tutorialProgress < tutorialNodes.Count)
        {
            dialogueRunner.StartDialogue(tutorialNodes[tutorialProgress]);
            tutorialProgress++;
        }
        else
        {
            SceneManager.LoadScene("Day1Scene");
        }
    }
}
