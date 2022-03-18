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

    private List<ItemSlot> itemSlotList = new List<ItemSlot>();
    public GameObject itemSlotPrefab;
    public Transform invItemTransform;

    // Start is called before the first frame update
    private void Start()
    {
        Inventory.instance.onItemChange += UpdateInventoryUI;
        UpdateInventoryUI();

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

    private void UpdateInventoryUI()
    {
        // count of items in the Inventory
        int count = Inventory.instance.invItemList.Count;

        if (count > itemSlotList.Count)
        {
            AddItemSlots(count);

        }

        for (int i = 0; i < itemSlotList.Count; i++)
        {
            if (i <= count)
            {
                itemSlotList[i].AddItem(Inventory.instance.invItemList[i]);
            } else
            {
                itemSlotList[i].DestroySlot();
                itemSlotList.RemoveAt(i);
            }
        }
    }

    private void AddItemSlots(int count)
    {
        // amount of slots we need 
        int amt = count - itemSlotList.Count;
        for (int i = 0; i < amt; i++)
        {
            GameObject itemSlotObj = Instantiate(itemSlotPrefab, invItemTransform);
            ItemSlot newSlot = itemSlotObj.GetComponent<ItemSlot>();
            itemSlotList.Add(newSlot);
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
