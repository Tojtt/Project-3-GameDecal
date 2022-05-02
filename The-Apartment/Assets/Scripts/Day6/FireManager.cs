using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public GameObject redGlobalLight;
    public GameObject normalGlobalLight;
    public GameObject playerLight;

    void Awake()
    {
        //redGlobalLight = GameObject.Find("GlobalLight2D-REDHOTFIRE");
       // playerLight = GameObject.Find("GlobalLight-PlayerOnly");
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
        normalGlobalLight.SetActive(false);
        redGlobalLight.SetActive(true);
        TurnOffHallwayLights();
        //  playerLight.SetActive(true);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    void TurnOffHallwayLights()
    {
        GameObject lights = GameObject.Find("HallwayLights");

        foreach (Transform child in lights.transform)
        {
            GameObject l = child.gameObject;
            l.GetComponent<LightScript>().turnOff();
        }
    }
    #endregion
}
