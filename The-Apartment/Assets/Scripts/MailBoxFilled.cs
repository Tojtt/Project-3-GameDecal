using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBoxFilled : MonoBehaviour
{
    #region Variables
    //NOTE: No collider on item!
    public GameObject item; //Item inside mailbox, tag as "MailboxCollectible", not "collectible"
    BoxCollider2D itemCollider;
    bool opened = false;
    bool itemTaken = false;
    bool mouseLifted = false;
    InventoryManager invM;
    GameState gameState;
    #endregion

    #region Unity_Functions
    void Awake()
    {
        item.SetActive(false);
        invM = GameObject.FindWithTag("Inventory").GetComponent<InventoryManager>();
        gameState = GameObject.FindWithTag("GameManager").GetComponent<GameState>();
    }

    void OnMouseDown()
    {
        mouseLifted = false;
        if (!opened)
        {
            OpenMailBox();
        }
        else if (!itemTaken)
        {
            TakeItem();
        } else 
        {
            CloseMailBox();
        }
    }

    void OnMouseUp()
    {
        mouseLifted = true;
        if (opened && !itemTaken)
        {
            item.SetActive(true);
        }
        if (!opened && !itemTaken)
        {
            item.SetActive(false);
        }
    }
    #endregion

    void OpenMailBox()
    {
        
        //Change sprite appearance
        opened = true;
        Debug.Log("Opened mailbox");
    }

    void TakeItem()
    {
        Item itemScript = item.GetComponent<Item>();
        if (itemScript.itemID == 12) //Cash envelope
        {
            gameState.moneyEarned += 100;
            Debug.Log("Earned $100");
        } else
        {
            invM.AddItem(itemScript); 
        }
        item.SetActive(false);

        itemTaken = true;
        Debug.Log("Item taken");

    }
    void CloseMailBox()
    {
        
        //Change sprite appearance or add coroutine
        opened = false;
        Debug.Log("Closed mailbox");
    }
}
