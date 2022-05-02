using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OpenBook : MonoBehaviour
{
    public Image image1;
    public Image image2;
    bool opened = false;
    bool doneOpening = false;
    bool coroutining = false;
    bool doneClosing = true;

    void Awake()
    {
        image1.enabled = true;
        image2.enabled = true;
        SetAlpha(image1, 0f);
        SetAlpha(image2, 0f);
        //
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && opened && doneOpening)
        {
            StartCoroutine(Disappear());
            opened = false;
            doneClosing = false;
        }
    }   

    void OnMouseDown()
    {
        if (!opened && doneClosing)
        {
            opened = true;
            doneOpening = false;
            StartCoroutine(Appear());
        }
    }

    void SetAlpha(Image image, float alpha)
    {
        Color newColor = image.color;
        newColor.a = alpha;
        image.color = newColor;
    }

    IEnumerator Appear()
    {
        for (int i = 1; i < 6; i++)
        {
            SetAlpha(image1, i * 0.13f);
            SetAlpha(image2, i * 0.2f);
            yield return new WaitForSeconds(0.1f);
        }
        doneOpening = true;
    }

    IEnumerator Disappear()
    {
        for (int i = 1; i < 6; i++)
        {
            SetAlpha(image1, 0.65f - i * 0.13f);
            SetAlpha(image2, 1f - i * 0.2f);
            yield return new WaitForSeconds(0.1f);
        }
        doneClosing = true;
    }
}
