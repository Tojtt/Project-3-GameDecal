using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SpecialLight : MonoBehaviour
{
    // Start is called before the first frame update
    private float intensity;
    private bool on;
    Light2D thisLight;

    void Start()
    {
        on = true;
        intensity = 1;
        GameObject childLight = this.gameObject.transform.GetChild(0).gameObject;
        thisLight = childLight.GetComponent<Light2D>();
        turnOff();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void turnOn()
    {
        if (!on)
        {
            on = true;
            thisLight.intensity = intensity;
        }
    }

    public void turnOff()
    {
        if (on)
        {
            on = false;
            thisLight.intensity = 0;
        }
    }
    public void addIntensity(float add)
    {
        on = true;
        thisLight.intensity += add;
    }
}
