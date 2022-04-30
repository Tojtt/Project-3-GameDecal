using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeMat : MonoBehaviour
{
    //SET ISEMPTY TO TRUE!!
    #region Editor_Variables
    public GameObject item;
    BoxCollider2D itemCollider;
    public bool isEmpty;
    public Sprite openedSprite;
    public Sprite closedSprite;
    #endregion

    #region Drawer_variables
    bool opened = false;
    bool itemTaken = false;
    bool unlocked = false;

    Vector3 shiftPosition = new Vector3(1.2f, -0.3f, 0);
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
        itemCollider = item.gameObject.GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
        if (item != null)
        {
            //itemCollider.isTrigger = false;
            itemCollider.enabled = false;
            
            item.SetActive(false);
            
        }
        else
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
            OpenDrawer();
            
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

    void OnMouseUp()
    {
        if (opened && !itemTaken)
        {
            itemCollider.enabled = true;
        }
    }
    #endregion

    #region Interact_functions
    void OpenDrawer()
    {
        renderer.sprite = openedSprite;

        gameObject.transform.position += shiftPosition;
        opened = true;
        Debug.Log("Opened drawer");
        item.SetActive(true);
    }

    void TakeItem()
    {
        Item itemScript = item.GetComponent<Item>();
        invM.AddItem(itemScript);

        item.SetActive(false);
        itemTaken = true;
        Debug.Log("Item taken");
    }

    void CloseDrawer()
    {
        gameObject.transform.position -= shiftPosition;
        renderer.sprite = closedSprite;
        opened = false;
        Debug.Log("Closed drawer");
    }

    public void Unlock()
    {
        unlocked = true;
    }
    #endregion
}
