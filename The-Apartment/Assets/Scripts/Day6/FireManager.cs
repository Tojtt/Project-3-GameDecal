using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    GameObject redGlobalLight;
    GameObject playerLight;

    void Awake()
    {
        redGlobalLight = GameObject.Find("GlobalLight2D-REDHOTFIRE");
        playerLight = GameObject.Find("GlobalLight-PlayerOnly");
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        
        StartFire();
    }

    #region Fire_functions
    public void StartFire()
    {
        redGlobalLight.SetActive(true);
        playerLight.SetActive(true);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
    #endregion
}
