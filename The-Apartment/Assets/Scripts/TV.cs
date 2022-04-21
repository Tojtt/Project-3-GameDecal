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
        //camera.GetComponent<CameraFollow>().SwitchToPosition(target);
        //Camera maincam = GameObject.Find("Main Camera").GetComponent<Camera>();
        maincam.gameObject.GetComponent<CameraFollow>().followEnabled = false;

        maincam.transform.position = target;
        //player.transform.position = tvOverlay.GetComponent<Teleporter>().GetDestination();
        GameState.Instance.freezePlayer = true;
        Debug.Log("Moved player");
        if (!dialogue.IsDialogueRunning)
        {
            dialogue.StartDialogue("TVDay0");
        }
        //maincam.gameObject.GetComponent<CameraFollow>().followEnabled = true;
        GameState.Instance.freezePlayer = false;
        // reset position back to the previously recorded position 
        //maincam.transform.position = prevPos;

        // run TV cutscene here?
    }
}
