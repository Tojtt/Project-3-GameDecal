using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
        // Start is called before the first frame update
    #region Movement_variables
    public float movespeed;
    float defaultMoveSpeed;
    float roomMoveSpeed = 2.5f;
    float x_input;
    float y_input;
    Vector2 currDirection;
    bool move2D = false;
    #endregion

    #region Position_variables

    public Vector2 homePosition = new Vector2(30f, 3f);
    
    #endregion

    #region Physics_components
    Rigidbody2D PlayerRB;
    BoxCollider2D collider;
    #endregion

    #region GameObject_components
    //Animator anim;
    GameState gameState;
    GameManager gm;
    GameObject gameManager;

    private GameObject doorTeleporter;
    private GameObject stairTeleporter;
    #endregion

    #region UI
    Text floorText;
    #endregion

    #region CameraSize_Variables
    float defaultCameraSize = 5;
    float zoomedInSize = 3.5f; //3.5 or 4
    #endregion

    #region Unity_functions

    private void Awake()
    {
        //>>>>>NOTE: In inspector, make sure "Order in Sorting Layer" = 1
        defaultMoveSpeed = movespeed;
        PlayerRB = GetComponent<Rigidbody2D>();
        floorText = GameObject.Find("FloorDescription").GetComponent<UnityEngine.UI.Text>();
        collider = GetComponent<BoxCollider2D>();
        //anim = GetComponent<Animator>();

        Camera.main.orthographicSize = defaultCameraSize;
    }

    private void Start() 
    {
        //get GameManager and loadlevel object
        gameManager = GameObject.FindWithTag("GameManager");
        
        //then pull the script from the object
        gm = gameManager.GetComponent<GameManager>();
        gameState = gameManager.GetComponent<GameState>();
        gameState.floor = 2;
        Debug.Log("good");
    }
    private void Update()
    {
        //get inputs from keyboard
        x_input = Input.GetAxisRaw("Horizontal");
        y_input = Input.GetAxisRaw("Vertical");
        Move();
        if (Input.GetKeyDown(KeyCode.D))
        {
            NightTransition(); //<< Are we still using this?
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoTeleport();
        }
    }
    #endregion

    //Note: Put functions for update in here, instead of cluttering Update
    #region Update_functions
    void NightTransition()
    {
        if (gameState.dayFinished && SceneManager.GetActiveScene().name == "Apartment")
        {
            gm.nightTransition();
        }
        else
        {
            //Debug.Log("Need to finish tasks first");
        }
    }

    void DoTeleport()
    {
        if (doorTeleporter != null)
        {
            transform.position = doorTeleporter.GetComponent<Teleporter>().GetDestination().position;

            if (doorTeleporter.transform.name == "Apt Door") //Teleport into main character's room
            {
                SetRoomVariables(206);
            }
            else if (doorTeleporter.transform.name == "HallwayDoor206") //Teleport from mainCharacter's room into Hallway
            {
                SetHallwayVariables();  
            }

            StartCoroutine("Teleport");
        }

        if (stairTeleporter != null)
        {
            if (stairTeleporter.transform.name.Contains("up"))
            {
                gameState.floor += 1;
                floorText.text = "Floor " + gameState.floor.ToString();

            }

            else
            {

                gameState.floor -= 1;
                if (gameState.floor == 0)
                {
                    floorText.text = "Basement";
                }
                else
                {
                    floorText.text = "Floor " + gameState.floor.ToString();
                }


            }
            transform.position = stairTeleporter.GetComponent<Teleporter>().GetDestination().position;
        }
    }
    
    void SetRoomVariables(int roomNum)
    {
        move2D = true;
        movespeed = roomMoveSpeed;
        Debug.Log(gameState);
        gameState.inRoom = true;
        gameState.roomNum = 206;
        Camera.main.orthographicSize = zoomedInSize;
        Debug.Log("Zoomed in");
    }

    void SetHallwayVariables()
    {
        move2D = false;
        movespeed = defaultMoveSpeed;
        gameState.inRoom = false;
        gameState.roomNum = -1;
        Camera.main.orthographicSize = defaultCameraSize;
    }
    #endregion

    #region Movement_functions
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
        if (coll.CompareTag("Stair"))
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

        
        //yield return StartCoroutine(levelLoader.GetComponent<FadeScript>().FadeIn()); <<<<<-Do this without levelLoader
        yield return transform.position = doorTeleporter.GetComponent<Teleporter>().GetDestination().position;
        //yield return StartCoroutine(levelLoader.GetComponent<FadeScript>().FadeOut());

    }

    #endregion

    #region Getter_Functions
    public Vector2 GetDirection()
    {
        return currDirection;
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public float GetColliderY()
    {
        return collider.bounds.center.y;// + (collider.size.y / 2);// - collider.offset.y;
    }
    #endregion


}

