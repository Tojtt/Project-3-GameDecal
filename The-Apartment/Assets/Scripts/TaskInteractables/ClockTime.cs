using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockTime : MonoBehaviour
{
    int hour = 11; //12:__

    [SerializeField]
    public Sprite[] clockTimes;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = clockTimes[hour];
    }

    void OnMouseDown()
    {
        hour++;
        hour %= 12;
        spriteRenderer.sprite = clockTimes[hour];   
    }

    public int GetHour()
    {
        return hour + 1;
    }
}
