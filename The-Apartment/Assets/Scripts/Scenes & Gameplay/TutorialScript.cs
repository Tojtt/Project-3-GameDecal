using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class TutorialScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public GameObject friend;
    public DialogueRunner dialogueRunner;
    //public LineView lineview;
    private int tutorialProgress;
    [SerializeField]
    public List<String> tutorialNodes;

    #region Cutscene_Variables
    bool hasRun = false;
    public bool canMove = false;
    Vector3 startPosition;
    #endregion

    void Start()
    {
        //playerChar = GameObject.Find("Player");
        //friendChar = GameObject.Find("Friend");

        //dialogueRunner = FindObjectOfType<DialogueRunner>();
        //lineview = FindObjectOfType<LineView>();
        startPosition = player.transform.position;
        tutorialProgress = 0;
        dialogueRunner.onDialogueComplete.AddListener(ProgressTutorial);
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialProgress == 0 && hasRun)
        {
            // update position 
            float speed = 10f;
            Vector3 targetDest = new Vector3(friend.transform.position.x + 1.5f, startPosition.y);
            Debug.Log(targetDest);
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetDest, speed * Time.deltaTime);
            Debug.Log(player.transform.position);
        }

        if (canMove)
        {
            // update functions
        }
    }

    void ProgressTutorial ()
    {
        dialogueRunner.Stop();

        // first stage - player is manually walked to where the friend is
        if (tutorialProgress == 0)
        {
            hasRun = true;
            StartCoroutine(WalkToFriend());
        } else if (tutorialProgress == 1)
        {
            // enable player movement
            enableMovement();
        } else
        {
            SceneManager.LoadScene("Day1Scene");
        }
    }

    public IEnumerator WalkToFriend()
    {
        yield return new WaitForSeconds(5.0f);
        // began dialogue after
        dialogueRunner.Stop();
        if (!dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.StartDialogue(tutorialNodes[tutorialProgress]);
            tutorialProgress++;
        }

    }

    private void enableMovement()
    {
        canMove = true;
    }

    private void OnMouseDown()
    {
        MenuTransition.EndTutorial();
    }
}
