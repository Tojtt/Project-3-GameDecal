using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OpenBook : MonoBehaviour
{
    public Image image1;
    public Image image2;
    void Awake()
    {
        image1.enabled = false;
        image2.enabled = false;
    }
    void OnMouseDown()
    {
        //TODO: Add fade in 
        image1.enabled = true;
        image2.enabled = true;
    }
}
