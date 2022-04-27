using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{

    public Item item;
    //public String itemName;
    public Image itemImage;
    public Button button;
    Image image;

    void Awake()
    {
        button = this.GetComponent<Button>();
        image = gameObject.GetComponent<Image>();
    }

    public int getItemID()
    {
        return item.itemID;
    }

    public void SetIcon() //SET RAYCAST TARGET TO FALSE
    {
        Image itemIcon = this.gameObject.transform.GetChild(0).GetComponent<Image>();
        itemIcon.sprite = item.sprite;
        itemIcon.rectTransform.sizeDelta *= 3.5f; //Scaling icon
    }
}
