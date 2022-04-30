using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour
{
    public int keyID;
    InventoryManager invM;
    bool unlocked = false;
    Drawer drawerScript;

    void Awake()
    {
        invM = GameObject.FindWithTag("Inventory").GetComponent<InventoryManager>();
        drawerScript = gameObject.GetComponent<Drawer>();
    }

    void OnMouseDown()
    {
        if (!unlocked)
        {
            if (invM.GetSelectedID() == keyID)
            {
                drawerScript.Unlock();
                unlocked = true;
            }
        }
    }
}
