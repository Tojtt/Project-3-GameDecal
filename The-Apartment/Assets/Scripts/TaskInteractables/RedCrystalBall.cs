using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCrystalBall : MonoBehaviour
{
    //Actually just script for symbol itself
    private Material material;
    SpecialLight lightScript;
    void Awake()
    {
        //secretSymbol.SetActive(false);
        material = this.GetComponent<Renderer>().material;
        lightScript = GameObject.Find("CrystalLightRed").GetComponent<SpecialLight>();
        lightScript.turnOff();
        SetAlpha(0f);
        Debug.Log("uy");
    }

    void SetAlpha(float alpha)
    {
        Color newColor = material.color;
        newColor.a = alpha;
        material.color = newColor;
    }

    public IEnumerator Appear()
    {
        for (int i = 1; i < 6; i++)
        {
            SetAlpha(i * 0.5f);
            lightScript.addIntensity(0.2f);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
