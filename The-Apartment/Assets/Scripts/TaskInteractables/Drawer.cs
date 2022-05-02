using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    #region Editor_Variables
    public GameObject item; 
    public bool containsCollectible;
    public bool isLocked;
    public bool isEmpty;
    public bool invisibleSprite; //If the open sprite should be invisible instead of replaced
    public Sprite openedSprite;
    public Sprite closedSprite;

    public Sprite itemNewSprite;
    #endregion

    #region Drawer_variables
    bool opened = false;
    bool itemTaken = false;
    bool unlocked = false;

    BoxCollider2D itemCollider;
    #endregion

    #region Referenced_Variables
    InventoryManager invM;
    GameState gameState;
    SpriteRenderer playerRenderer;
    #endregion


    #region Appearance_variables
    SpriteRenderer renderer;
    #endregion


    #region Unity_functions
    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        if (!isEmpty) {
            item.SetActive(false);
            
            itemCollider = item.GetComponent<BoxCollider2D>();
            if (itemCollider)
            {
                itemCollider.enabled = false;
            }
                
            if (!item.GetComponent<Item>().notDroppable)
            {
                playerRenderer = GameObject.FindWithTag("Player").GetComponent<PlayerController>().GetComponent<SpriteRenderer>();
            }
        }
        
        if (invisibleSprite)
        {
            renderer.enabled = false; //Set invisible
        } else
        {
            renderer.sprite = closedSprite;
        }

        
        invM = GameObject.FindWithTag("Inventory").GetComponent<InventoryManager>();
        gameState = GameObject.FindWithTag("GameManager").GetComponent<GameState>();
    }

    void OnMouseDown()
    {
        if (!opened)
        {
            if (!isLocked || unlocked)
            {
                OpenDrawer();
            }
        } else if (!isEmpty && !itemTaken)
        {
            TakeItem();
        } else
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
            if (!itemScript.notDroppable)
            {
                //Change tag of item to collectible
                itemScript.gameObject.tag = "Collectible";
                itemCollider.enabled = true;
                itemScript.SetOrder(playerRenderer.sortingOrder - 1);
                if (itemScript.itemID == 3) //Lighter
                {
                    item.GetComponent<SpriteRenderer>().sprite = itemNewSprite;
                }
            }
        } else //Contains MONEY
        {
            Money moneyScript = item.GetComponent<Money>();
            gameState.EarnMoney(moneyScript.amountMoney);
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
    }
    #endregion
}
