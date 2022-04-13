using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    public int roomNum;
    private SpriteRenderer sprite;

    #region Collider_Variables
    BoxCollider2D boxCollider2D;
    float y; //Doesn't change
    int order = 0;
    #endregion

    #region Referenced_variables
    PlayerController player;
    GameState gameState;
    #endregion

    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        y = boxCollider2D.bounds.center.y;// + (boxCollider2D.size.y / 2);
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        gameState = GameObject.FindWithTag("GameManager").GetComponent<GameState>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.inRoom)
        {
            float player_y = player.GetColliderY();
            
            if (order != 2 && player_y > y) //Player is behind furniture
            {
                Debug.Log(player_y.ToString() + " " + y.ToString());
                Debug.Log("Furniture move forward");
                sprite.sortingOrder = 2;
                order = 2;
            }
            else if (order != 0 && player_y < y)
            {
                Debug.Log(player_y.ToString() + y.ToString());
                Debug.Log("Furniture move backward");
                sprite.sortingOrder = 0;
                order = 0;
            }
        }
    }


}
