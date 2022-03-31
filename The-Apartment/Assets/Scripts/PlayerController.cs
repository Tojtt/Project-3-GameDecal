using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
        // Start is called before the first frame update
    #region Movment_variables
    public float movespeed;
    float x_input;
    float y_input;
    Vector2 currDirection;
    #endregion

    #region Physics_components
    Rigidbody2D PlayerRB;
    #endregion

    #region GameObject_components
    //Animator anim;
    GameState gs;
    GameManager gm;
    LevelLoader load;
    GameObject gameManager;
    GameObject levelLoader;
    #endregion


    #region Unity_functions

    private void Awake()
    {
        
        PlayerRB = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }

    private void Start() 
    {
        //get GameManager and loadlevel object
        gameManager = GameObject.FindWithTag("GameManager");
        levelLoader = GameObject.FindWithTag("LevelLoader");
        //then pull the script from the object
        gm = gameManager.GetComponent<GameManager>();
        gs = gameManager.GetComponent<GameState>();   
        load = levelLoader.GetComponent<LevelLoader>(); 
    }
    private void Update()
    {
        //get inputs from keyboard
        x_input = Input.GetAxisRaw("Horizontal");
        y_input = Input.GetAxisRaw("Vertical");
        Move();

        if (Input.GetKeyDown(KeyCode.D))
        {
            if(gs.dayFinished && SceneManager.GetActiveScene().name == "Apartment")
            {
                gm.nightTransition();   
            }
            else 
            {
                Debug.Log("Need to finish tasks first");
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space key pressed");
            Interact();
            
        }


    }
    #endregion

    #region Movment_functions

    private void Move()
    {
        
        //anim.SetBool("Moving", true);  
        if (x_input > 0)
        {
            //Debug.Log("input working right?");
            PlayerRB.velocity = Vector2.right * movespeed;
            currDirection = Vector2.right;

        }
        else if (x_input < 0)
        {
            PlayerRB.velocity = Vector2.left * movespeed;
            currDirection = Vector2.left;
        }

        else if (y_input > 0 && SceneManager.GetActiveScene().name != "Hallway")
        {
            PlayerRB.velocity = Vector2.up * movespeed;
            currDirection = Vector2.up;
        }
        else if (y_input < 0 && SceneManager.GetActiveScene().name != "Hallway")
        {
            PlayerRB.velocity = Vector2.down * movespeed;
            currDirection = Vector2.down;
        }
        else
        {
            PlayerRB.velocity = Vector2.zero;
            //anim.SetBool("Moving", false);
        }
        //anim.SetFloat("DirX", currDirection.x);
        //anim.SetFloat("DirY", currDirection.y);

    }
    #endregion

    #region Interact_functions

    
    //cast a invisible vector in front to trigger the items
    private void Interact()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(PlayerRB.position + currDirection, Vector2.one, 0f, Vector2.zero, 0f);
        // foreach (RaycastHit2D hit in hits)
        // {
        //     if(hit.transform.CompareTag("Item"))
        //     {
        //         hit.transform.GetComponent<Item>().Interact();
        //     }
        // }
    }
    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("ApartmentDoor"))
        {
            if(SceneManager.GetActiveScene().name == "Apartment")
            {
                gm.Hallway();
            }

            if(SceneManager.GetActiveScene().name == "Hallway")
            {
                gm.Apartment();
            }    
        }

        if (coll.CompareTag("Stair"))
        {
        }
    }

    #endregion
    

}