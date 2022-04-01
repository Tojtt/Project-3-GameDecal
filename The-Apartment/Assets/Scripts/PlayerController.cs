using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
        // Start is called before the first frame update
    #region Movment_variables
    public float movespeed;
    float x_input;
    float y_input;
    Vector2 currDirection;
    bool move2D = false;
    
    bool atHomeDoor = false;
    bool inHome =  false;
    #endregion

    #region Position_variables

    public Vector2 homePosition = new Vector2(30f, 3f);
    
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

    private GameObject doorTeleporter;
    private GameObject stairTeleporter;
    #endregion

    #region UI
    Text floorText;
    #endregion

    int floor;
    #region Unity_functions

    private void Awake()
    {
        
        PlayerRB = GetComponent<Rigidbody2D>();
        floorText = GameObject.Find("FloorDescription").GetComponent<UnityEngine.UI.Text>();
        //anim = GetComponent<Animator>();
    }

    private void Start() 
    {
        //get GameManager and loadlevel object
        gameManager = GameObject.FindWithTag("GameManager");
        levelLoader = GameObject.FindWithTag("LevelLoader");
        floor = 2;
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
            if(doorTeleporter != null)
            {
                transform.position = doorTeleporter.GetComponent<Teleporter>().GetDestination().position;

                if (doorTeleporter.transform.name == "Apt Door")
                { //Player room

                    move2D = true;
                }

                StartCoroutine("Teleport");
            }

            if(stairTeleporter != null)
            {
                if (stairTeleporter.transform.name.Contains("up"))
                {
                    floor += 1;
                    floorText.text = "Floor " + floor.ToString();

                }
                else if (stairTeleporter.transform.name == "HallwayDoor")
                { //Player room

                    move2D = false;
                }
                else
                {

                    floor -= 1;
                    if (floor == 0)
                    {
                        floorText.text = "Basement";
                    }
                    else
                    {
                        floorText.text = "Floor " + floor.ToString();
                    }


                }
                transform.position = stairTeleporter.GetComponent<Teleporter>().GetDestination().position;
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
        } else if (y_input > 0 && move2D)  //&& SceneManager.GetActiveScene().name != "HallwayLayout")
        {
            PlayerRB.velocity = Vector2.up * movespeed;
            currDirection = Vector2.up;
        }
        else if (y_input < 0 && move2D)  //&& SceneManager.GetActiveScene().name != "HallwayLayout")
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
    
    
    #endregion
    
    #region Teleporter_Destinations
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("homeTeleporter"))
        {
            doorTeleporter = coll.gameObject;
        }
        if (coll.CompareTag("Stair"))
        {
            stairTeleporter = coll.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("homeTeleporter"))
        {
            if (coll.gameObject == doorTeleporter)
            {
                doorTeleporter = null;
            }
        }
        if (coll.CompareTag("homeTeleporter"))
        {
            if (coll.gameObject == stairTeleporter)
            {
                stairTeleporter = null;
            }
        }
    } 

    IEnumerable Teleport()
    {
        Debug.Log("Teleport is running");

        
        //yield return StartCoroutine(levelLoader.GetComponent<FadeScript>().FadeIn());
        yield return transform.position = doorTeleporter.GetComponent<Teleporter>().GetDestination().position;
        //yield return StartCoroutine(levelLoader.GetComponent<FadeScript>().FadeOut());

    }

    #endregion
}