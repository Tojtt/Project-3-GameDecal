using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public bool inventoryOpen = false;

    public bool InventoryOpen => inventoryOpen;
    public GameObject invParent;
    public GameObject invTab;
    public GameObject craftTab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryOpen)
            {
                CloseInventory();
            } else
            {
                OpenInventory();
            }
        }
    }

    private void OpenInventory()
    {
        inventoryOpen = true;
        invParent.SetActive(true);
        OnInvTabClicked();
    }

    private void CloseInventory()
    {
        inventoryOpen = false;
        invParent.SetActive(false);
    }

    public void OnCraftTabClicked()
    {
        invTab.SetActive(false);
        craftTab.SetActive(true);
    }

    public void OnInvTabClicked()
    {
        invTab.SetActive(true);
        craftTab.SetActive(false);
    }

}
