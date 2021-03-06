using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MouseInput : MonoBehaviour
{
    public InventoryManager invM;

    GasManager gasManager;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            wp.z = 0;

            Collider2D[] allOverlaps = Physics2D.OverlapCircleAll(wp, 0.1f);

            bool clickedSlot = false;
            //Debug.Log(string.Format("Hit {0} objects", allOverlaps.Length));
            for (int i = 0; i < allOverlaps.Length; i++)
            {
                GameObject target = allOverlaps[i].gameObject;
                if (target.CompareTag("Collectible")) 
                {
                    ClickCollectible(target);
                    break;
                } 
                else if (target.CompareTag("Trash")) 
                {
                    ClickTrash(target);
                    break;
                } 
                else if (target.CompareTag("Floor")) 
                {
                    ClickFloor(target);
                } else if (target.CompareTag("ItemSlot"))
                {
                    clickedSlot = true;
                    break;
                }
            }

            if (!clickedSlot) //If clicked away from slot, deselect item
            {
                invM.DeselectItem();
            }
        }
    }

    //Put what happens when you click on a certain item here to not clutter Update + KEEP ORGANIZED
    #region ClickFunctions
    void ClickCollectible(GameObject target)
    {
        invM.AddItem(target.GetComponent<Item>());
        target.SetActive(false); //<<<< Don't destroy
        // Debug.Log("Added");

    }

    void ClickTrash(GameObject target)
    {
        Destroy(target);
        // Debug.Log("Trash has been disposed of");
    }

    void ClickFloor(GameObject target)
    {
        if (invM.GetSelectedID() == 1) //Gas Tank
        {
            Debug.Log(invM.GetSelectedID());
            if (gasManager == null)
            {
                gasManager = GameObject.FindWithTag("GasManager").GetComponent<GasManager>();
            }
            gasManager.BeginSpilling();

        } else if (invM.GetSelectedID() == 3) //Lighter
        {
            if (gasManager == null)
            {
                gasManager = GameObject.FindWithTag("GasManager").GetComponent<GasManager>();
            }
            gasManager.StartFire(true);
        }
    }
    #endregion
}