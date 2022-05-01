using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillow : MonoBehaviour
{
    //SET ISEMPTY TO TRUE!!
    #region Editor_Variables
    public GameObject item;
    Item itemScript;
    BoxCollider2D itemCollider;
    public bool isEmpty;
    #endregion

    #region Drawer_variables
    bool opened = false;
    //bool itemTaken = false;
    bool unlocked = false;

    public Vector3 shiftPosition;
    #endregion

    #region Referenced_Variables
    InventoryManager invM;
    GameState gameState;
    #endregion

    #region Appearance_variables
    SpriteRenderer renderer;
    #endregion


    #region Unity_functions
    void Awake()
    {
        
        renderer = GetComponent<SpriteRenderer>();
        if (item != null)
        {
            itemScript = item.GetComponent<Item>();
            itemCollider = item.gameObject.GetComponent<BoxCollider2D>();
            //itemCollider.isTrigger = false;
            itemCollider.enabled = false;

            item.SetActive(false);

        }
        else
        {
        }

        invM = GameObject.FindWithTag("Inventory").GetComponent<InventoryManager>();
        gameState = GameObject.FindWithTag("GameManager").GetComponent<GameState>();
    }

    void OnMouseDown()
    {
        if (!opened)
        {
            OpenDrawer();

        }
        else
        {

            CloseDrawer();
        }
    }

    void OnMouseUp()
    {
        if (item != null && opened && !itemScript.wasTaken())
        {
            itemCollider.enabled = true;
        }
    }
    #endregion

    #region Interact_functions
    void OpenDrawer()
    {
        Debug.Log("???");
        Debug.Log(gameObject.transform.position);
        gameObject.transform.position += shiftPosition;
        opened = true;
        Debug.Log("Opened drawer");
        if (item != null)
        {
            item.SetActive(true);
        }
        
    }

    //void TakeItem()
    //{
    //    Item itemScript = item.GetComponent<Item>();
    //    invM.AddItem(itemScript);

    //    item.SetActive(false);
    //    itemTaken = true;
    //    Debug.Log("Item taken");
    //}

    void CloseDrawer()
    {
        gameObject.transform.position -= shiftPosition;
        opened = false;
        Debug.Log("Closed drawer");
        if (item != null && !itemScript.wasTaken())
        {
            itemCollider.enabled = false;
        }
    }
    #endregion
}
