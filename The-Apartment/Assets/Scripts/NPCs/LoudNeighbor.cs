using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class LoudNeighbor : MonoBehaviour
{
    #region Appearance_Variables
    public Sprite closedDoorSprite;
    public Sprite openDoorSprite;
    SpriteRenderer spriteRenderer;
    #endregion

    #region Dialogue Variables
    public string nearbyDialogueNode;
    public string knockDoorDialogueNode;
    public string excuseDialogueNode;
    
    bool wasOutsideRange = true;
    #endregion

    #region Editor_Variables
    [SerializeField]
    public DialogueRunner dialogueRunner;
    public float triggerDistance = 3.0f; //For door
    #endregion
    
    #region Game_Variables
    int roomNum = 205;
    int firstDay = 1;
    int emptyDay = 4;
    int lastDay = 2;
    public int doorTimes = 0;
    public string done;
    #endregion


    #region Referenced_Variables
    GameObject player;
    GameState gameState;
    Rigidbody2D rigidBody;
    DoorManager doorManager;
    InMemoryVariableStorage varStorage;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        gameState = GameObject.FindWithTag("GameManager").GetComponent<GameState>();
        varStorage = GameObject.Find("Dialogue System").GetComponent<InMemoryVariableStorage>();
        doorManager = GameObject.Find("DoorManager").GetComponent<DoorManager>();
        //Set to closed door
        spriteRenderer.sprite = closedDoorSprite;


    }
    // Update is called once per frame
    void Update()
    {
        if (gameState.day >= firstDay && gameState.day <= emptyDay) //Crack open door
        {
            CheckNearby();
            if (gameState.freezePlayer)
            {
                TryToUnfreeze();
            }
        }

    }

    #region Behavior_Functions
    void CheckNearby()
    {
        if (gameState.floor == roomNum / 100)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < triggerDistance)
            {
                if (wasOutsideRange)//!gameState.inDialogue)
                {
                    RandomDialogue();
                }
            }
            else
            {
                wasOutsideRange = true;
            }
        }
    }

    void RandomDialogue()
    {
        Debug.Log("random loud dialogue!");
        wasOutsideRange = false;
        //Dialogue
        Debug.Log(nearbyDialogueNode);
        dialogueRunner.StartDialogue(nearbyDialogueNode);
    }


    public void InteractNeighbor()
    {
        doorTimes++;
        dialogueRunner.Stop();
        spriteRenderer.sprite = openDoorSprite;
        gameState.freezePlayer = true;
        if(doorTimes == 1)
        {
            //Dialogue
            varStorage.SetValue("$done", 0);
            Debug.Log(knockDoorDialogueNode);
            dialogueRunner.StartDialogue(knockDoorDialogueNode);

        } else 
        {
            //Dialogue
            varStorage.SetValue("$done", 0);
            Debug.Log(excuseDialogueNode);
            dialogueRunner.StartDialogue(excuseDialogueNode);
        }
        
    }
    
    void TryToUnfreeze()
    {

        done = GetDialogueVariable("$done");
        if (done == "1") //Dialogue Finished
        {
            spriteRenderer.sprite = closedDoorSprite;
            Debug.Log("stopped");
            gameState.freezePlayer = false;
        }
        
    }

    void OnMouseDown()
    {
        if (doorManager.InClickRange(transform.position))
        {
            if (doorTimes >= 0 && !gameState.freezePlayer) //Knocking 
            {
                InteractNeighbor();
            }
        }
    }

    //Gets variables from dialogue
    string GetDialogueVariable(string var)
    {
        float value = 0;
        varStorage.TryGetValue(var, out value);
        return value.ToString();
    }
    #endregion


}
