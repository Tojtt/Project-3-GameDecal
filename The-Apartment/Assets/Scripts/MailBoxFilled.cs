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
    InventoryManager invM;
    GameState gameState;
    #endregion

    #region Appearance_variables
    public Renderer renderers;
    #endregion

    #region Unity_Functions
    void Awake()
    {
        renderers = this.GetComponent<Renderer>();
        renderers.enabled = false;
        item.SetActive(false);
        invM = GameObject.FindWithTag("Inventory").GetComponent<InventoryManager>();
        gameState = GameObject.FindWithTag("GameManager").GetComponent<GameState>();
    }

    void OnMouseDown()
    {
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

    #endregion

    void OpenMailBox()
    {
        renderers.enabled = true;
        if (!itemTaken)
        {
            item.SetActive(true);
        }
        
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
        renderers.enabled = false;
        ////Change sprite appearance or add coroutine
        //Color color = renderer.material.color;
        //color.a = 0;  //Make open sprite invisible
        //renderer.material.color = color;
        opened = false;
        Debug.Log("Closed mailbox");
    }

    //LATER: ADD COROUTINE TO MAILBOX, so opening/closing takes time
}
