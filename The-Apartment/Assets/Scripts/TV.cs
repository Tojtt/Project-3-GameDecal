using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TV : MonoBehaviour
{
    #region Unity_variables
    public SceneTransitions sceneTransition;
    public GameState gs;
    public GameObject player;
    public DialogueRunner dialogue;
    public GameObject tvOverlay;
    public GameObject maincam;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
        sceneTransition = FindObjectOfType<SceneTransitions>();
        dialogue = FindObjectOfType<DialogueRunner>();
        maincam = GameObject.Find("Main Camera");

    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogue.IsDialogueRunning)
        {
            maincam.gameObject.GetComponent<CameraFollow>().followEnabled = true;

        }
    }

    private void OnMouseDown()
    {
        // Load TV scene
        Debug.Log("TV Clicked");
        GameState.Instance.watchedTV = true;

        // get position of TV overlay and switch Camera to there
        Vector3 prevPos = player.transform.position;
        Vector3 target = tvOverlay.transform.position;
        Debug.Log("Current position" + target);
        maincam.gameObject.GetComponent<CameraFollow>().followEnabled = false;

        maincam.transform.position = target;
        GameState.Instance.freezePlayer = true;
        Debug.Log("Moved player");
        if (!dialogue.IsDialogueRunning)
        {
            // runs the node associated with the current day, or if its broken beyond Day 3
            string node = GameState.Instance.day <= 3 ? "TVDay" + GameState.Instance.day : "TVBroken";
            dialogue.StartDialogue(node);
        }
        GameState.Instance.freezePlayer = false;

        // run TV cutscene here?
    }
}
