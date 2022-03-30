using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBarControl : MonoBehaviour
{
    public int HotbarSlotSize => gameObject.transform.childCount;
    private List<ItemSlot> hotbarSlots = new List<ItemSlot>();

    KeyCode[] hotbarKeys = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5 };

    private void Start()
    {
        SetUpHotbarSlots();
    }

    private void Update()
    {
        for (int i = 0; i < hotbarKeys.Length; i++)
        {
            if (Input.GetKeyDown(hotbarKeys[i]))
            {
                hotbarSlots[i].UseItem();
                return;
            }
        }
    }

    private void SetUpHotbarSlots()
    {
        for (int i = 0; i < HotbarSlotSize; i++)
        {
            ItemSlot slot = gameObject.transform.GetChild(i).GetComponent<ItemSlot>();
            hotbarSlots.Add(slot);
        }
    }
}
