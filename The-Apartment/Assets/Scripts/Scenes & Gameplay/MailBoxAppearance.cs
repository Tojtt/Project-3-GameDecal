using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBoxAppearance : MonoBehaviour
{
    GameState gameState;
    private SpriteRenderer sr;


    public Sprite normalSprite;
    public Sprite rustySprite;

    void Awake()
    {
        gameState = GameObject.FindWithTag("GameManager").GetComponent<GameState>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (gameState.day < 3)
        {
            sr.sprite = normalSprite;
        }
        else
        {
            sr.sprite = rustySprite;
        }
    }
}
