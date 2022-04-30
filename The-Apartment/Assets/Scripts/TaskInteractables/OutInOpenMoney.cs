using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutInOpenMoney : MonoBehaviour
{
    public int amountMoney;
    GameState gameState;

    void Awake()
    {
        gameState = GameObject.FindWithTag("GameManager").GetComponent<GameState>();
    }

    void OnMouseDown()
    {
        gameState.EarnMoney(amountMoney);
        Destroy(this.gameObject);
    }
}
