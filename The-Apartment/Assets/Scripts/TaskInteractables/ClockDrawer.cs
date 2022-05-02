using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ClockDrawer : MonoBehaviour
{
    #region Editor_Variables
    public GameObject item;
    public bool containsCollectible;
    public bool isLocked;
    public bool isEmpty;
    public bool invisibleSprite; //If the open sprite should be invisible instead of replaced
    public Sprite openedSprite;
    public Sprite closedSprite;

    public GameObject clock;

    public DialogueRunner dialogueRunner;
    public string completedFT2DialogueNode;
    public string completedFT2DialogueNodeAlt;
    #endregion

    #region Drawer_variables
    bool opened = false;
    bool itemTaken = false;
    bool unlocked = false;
    #endregion

    #region Referenced_Variables
    InventoryManager invM;
    GameState gameState;
    
    ClockTime clockScript;
    #endregion


    #region Appearance_variables
    SpriteRenderer renderer;
    #endregion


    #region Unity_functions
    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        if (!isEmpty)
        {
            item.SetActive(false);
        }

        if (invisibleSprite)
        {
            renderer.enabled = false; //Set invisible
        }
        else
        {
            renderer.sprite = closedSprite;
        }

        invM = GameObject.FindWithTag("Inventory").GetComponent<InventoryManager>();
        gameState = GameObject.FindWithTag("GameManager").GetComponent<GameState>();
        clockScript = clock.GetComponent<ClockTime>();
    }

    void OnMouseDown()
    {
        if (!unlocked && isLocked && clockScript.GetHour() == 7)
        {
            Unlock();
        }
        if (!opened)
        {
            if (!isLocked || unlocked)
            {
                OpenDrawer();
            }
        }
        else if (!isEmpty && !itemTaken)
        {
            TakeItem();
        }
        else
        {
            CloseDrawer();
        }
        
    }
    #endregion

    #region Interact_functions
    void OpenDrawer()
    {
        if (invisibleSprite)
        {
            renderer.enabled = true; //Set visible
        }
        else
        {
            renderer.sprite = openedSprite;
        }

        if (!isEmpty && !itemTaken)
        {
            item.SetActive(true);
        }
        opened = true;
        Debug.Log("Opened drawer");
    }

    void TakeItem()
    {
        Item itemScript = item.GetComponent<Item>();
        if (containsCollectible)
        {
            invM.AddItem(itemScript);
        }
        else //Contains MONEY
        {
            Money moneyScript = item.GetComponent<Money>();
            gameState.EarnMoney(moneyScript.amountMoney);
            Debug.Log("Fortune Telling part 2 completed!");
            gameState.fortuneTellingPart2Complete = true;

            dialogueRunner.Stop();
            if (gameState.fortuneTellingComplete)
            {
                dialogueRunner.StartDialogue(completedFT2DialogueNode);
            } else
            {
                dialogueRunner.StartDialogue(completedFT2DialogueNodeAlt);
            }
            
        }
        item.SetActive(false);
        itemTaken = true;
        Debug.Log("Item taken");
    }

    void CloseDrawer()
    {
        if (invisibleSprite)
        {
            renderer.enabled = false; //Set invisible
        }
        else
        {
            renderer.sprite = closedSprite;
        }
        opened = false;
        Debug.Log("Closed drawer");
    }

    public void Unlock()
    {
        unlocked = true;
        Debug.Log("Unlocked");
    }
    #endregion
}
