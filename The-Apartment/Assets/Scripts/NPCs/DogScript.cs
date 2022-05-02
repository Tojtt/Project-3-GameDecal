using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogScript : MonoBehaviour
{
    //NOTE: This script should be attached to dog's door, not dog!!!
    /** Todo:
     * 1. Turn off dog dialogue for dog doorif dog is outside room
     * 2. can you feed the dog?
     * 3. ADD acceleration/decceleration to dog movement
     * 4. Add in dialogue for dog door, when dog is inside!
     * 5. Change follow if holding food to follow if fed food
     */
    public Animator anim;
    private SpriteRenderer sr;

    float movespeed;
    private Rigidbody2D dogRB;
    float space = 2;

    #region Task_variables
    public bool holdingFood = false; //Player is holding food
    private float timer = 2;
    bool inRoom = false;
    int startDay = 1; //Change later 
    int endDay = 6;
    #endregion

    #region Referenced_Variables
    private PlayerController player;
    GameState gameState;
    GameObject dog;
    DoorManager doorManager;
    #endregion

    void Start()
    {
        
        gameState = GameObject.FindWithTag("GameManager").GetComponent<GameState>();
        if (gameState.day >= startDay && gameState.day <= endDay)
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
            movespeed = player.movespeed + 0.2f;
            
            dog = GameObject.Find("DogNPC");
            sr = dog.GetComponent<SpriteRenderer>();
            dogRB = dog.GetComponent<Rigidbody2D>();
            doorManager = GameObject.Find("DoorManager").GetComponent<DoorManager>();
        }
    }

    void Update()
    {
        if (gameState.day >= startDay && gameState.day <= endDay)
        {
            if (holdingFood)
            {
                //Follow if nearby, but at least some distance away
                FollowPlayer();
            }
            else
            {
                //Dog sits in place, ignoring player
                dogRB.velocity = Vector2.zero;

            }
        }

    }

    void CheckReachedDogHome()
    {
        Debug.Log("Reached dog's home!");
        //Freeze player, Do dog transform animation, dog goes in room <<< ADD ANIMATION HERE
        gameState.lostDogComplete = true;
        dog.SetActive(false);
        //}
    }

    void FollowPlayer()
    {
        float distance = Mathf.Abs(player.transform.position.x -dogRB.transform.position.x);
        if (distance > space)
        {
            if (player.transform.position.x < dogRB.transform.position.x) //Player to left
            {
                sr.flipX = false;
                dogRB.velocity = Vector2.left * movespeed;
                anim.SetFloat("Speed", 1);
            }
            else
            {
                sr.flipX = true;
                dogRB.velocity = Vector2.right * movespeed;
                anim.SetFloat("Speed", 1);
            }
        } else
        {
            dogRB.velocity = Vector2.zero;
            anim.SetFloat("Speed", 0);
        }
        
    }

    void OnMouseDown()
    {
        if (holdingFood)
        {
            if (doorManager.InClickRange(transform.position)) //If in range of door
            {
                CheckReachedDogHome();
            }
        }
    }
    //IEnumerator moveTowardPlayer()
    //{
    //    Vector3 directionToPlayer = player.transform.position - this.gameObject.transform.position;
    //    Vector2 moveVector = new Vector2(directionToPlayer.normalized[0], 0);
    //    rigidBody.velocity = moveVector * movespeed;
    //    yield return new WaitForSeconds(1f);
    //    rigidBody.velocity = new Vector2(0, 0);
    //}

}
