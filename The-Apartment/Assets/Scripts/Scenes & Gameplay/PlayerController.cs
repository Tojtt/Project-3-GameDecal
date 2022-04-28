using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
        // Start is called before the first frame update
    #region Movement_variables
    public float movespeed;
    float defaultMoveSpeed;
    float roomMoveSpeed = 2f;
    float x_input;
    float y_input;
    Vector2 currDirection;
    bool move2D = false;
    #endregion

    #region Teleport_variables
    float[] hallTeleportYs = new float[] { -27.8f, -15.8f, -1f, 13.4f, 1f }; //0(basement), f1, f2, f3, 4(outside)
    float teleportCooldown = 2f;
    public float cooldownThreshold = 1f;

    #endregion
    #region Position_variables

    public Vector2 homePosition = new Vector2(30f, 3f);
    
    #endregion

    #region Physics_components
    Rigidbody2D PlayerRB;
    BoxCollider2D colliders;
    #endregion

    #region GameObject_components
    public Animator anim;
    private SpriteRenderer sr;
    GameState gameState;
    GameManager gm;
    GameObject gameManager;

    private GameObject doorTeleporter;
    private GameObject stairTeleporter;
    public GameObject forcedTeleporter;
    public SceneTransitions sceneTransition;
    #endregion

    #region UI
    Text floorText;
    #endregion

    int floor;

    #region CameraSize_Variables
    float defaultCameraSize = 5;
    float zoomedInSize = 3.5f; //3.5 or 4
    #endregion

    #region Task_Variables
    public bool inFrontDogHome = false;
    #endregion

    #region Unity_functions

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        //>>>>>NOTE: In inspector, make sure "Order in Sorting Layer" = 1
        defaultMoveSpeed = movespeed;
        PlayerRB = GetComponent<Rigidbody2D>();
        floorText = GameObject.Find("FloorDescription").GetComponent<UnityEngine.UI.Text>();
        colliders = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        Camera.main.orthographicSize = defaultCameraSize;
    }

    private void Start() 
    {
        //get GameManager and scenetransitions object
        gameManager = GameObject.FindWithTag("GameManager");
        sceneTransition = FindObjectOfType<SceneTransitions>();
        
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
        teleportCooldown += Time.deltaTime;

        if (!gameState.freezePlayer)
        {
            Move();
        } else
        {
            PlayerRB.velocity = Vector2.zero;
            anim.SetFloat("Speed", 0);
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            NightTransition(); //<< Are we still using this?
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            
            StartCoroutine(DoTeleport());
        } else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(DownTeleport());
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

    public IEnumerator DoTeleport()
    {
        
        if (doorTeleporter != null && teleportCooldown > cooldownThreshold)
        {
            teleportCooldown = 0;
            StartCoroutine(sceneTransition.TeleportTransition());
            yield return new WaitForSeconds(0.5f);
            Transform destination = doorTeleporter.GetComponent<Teleporter>().GetDestination();
            if (doorTeleporter.transform.name == "Apt Door206") //Teleport into main character's room
            {
                SetRoomVariables(206);
                transform.position = destination.position;
            }
            else if (doorTeleporter.transform.name == "HallwayDoor206") //Teleport from mainCharacter's room into Hallway
            {
                SetHallwayVariables();
                transform.position = GetTeleportPosition(destination);
            }
            else if (doorTeleporter.transform.name == "TV Room")
            {
                gameState.watchedTV = true;
            }
            else if (doorTeleporter.transform.name == "Door302-FortuneTellerPart2")
            {
                SetRoomVariables(302);
                transform.position = destination.position;
            } else if (doorTeleporter.transform.name == "HallwayDoor302Part2")
            {
                SetHallwayVariables();
                transform.position = GetTeleportPosition(destination);
            }
            else if (doorTeleporter.transform.name == "HallwayDoor304")
            {
                SetHallwayVariables();
                transform.position = GetTeleportPosition(destination);
            }
            else
            {
                transform.position = destination.position;
            }

            StartCoroutine("Teleport");
        }

        if (stairTeleporter != null && teleportCooldown > cooldownThreshold)
        {
            teleportCooldown = 0;
            //Vector3 newPosition = doorTeleporter.GetComponent<Teleporter>().GetDestination().position;
            //newPosition.y = hallTeleportYs[gameState.floor];
            
            Transform destination = stairTeleporter.GetComponent<StairTeleporter>().GetUpDestination();
            if (destination != null)
            {
                StartCoroutine(sceneTransition.TeleportTransition());
                yield return new WaitForSeconds(0.5f);
                gameState.floor += 1;
                floorText.text = "Floor " + gameState.floor.ToString();
                transform.position = GetTeleportPosition(destination);//destination.position;
            }
            
        }
        yield return null;
    }
    Vector3 GetTeleportPosition(Transform destination)
    {
        Vector3 newPosition = destination.position;
        newPosition.y = hallTeleportYs[gameState.floor];
        return newPosition;
    }

    public IEnumerator DownTeleport()
    {
        if (stairTeleporter != null)
        {
            
            Transform destination = stairTeleporter.GetComponent<StairTeleporter>().GetDownDestination();
            if (destination != null)
            {
                StartCoroutine(sceneTransition.TeleportTransition());
                yield return new WaitForSeconds(0.5f);
                gameState.floor -= 1;
                if (gameState.floor == 0)
                {
                    floorText.text = "Basement";
                }
                else
                {
                    floorText.text = "Floor " + gameState.floor.ToString();
                }
                transform.position = GetTeleportPosition(destination);
                //transform.position = destination.position;
            }
            
        }
    }
    public void ForcedTeleport()
    {
        if (forcedTeleporter != null)
        {
            Transform destination = forcedTeleporter.GetComponent<Teleporter>().GetDestination();
            if (forcedTeleporter.transform.name == "Door302-FortuneTeller")
            {
                SetRoomVariables(302);
                transform.position = destination.position;
            } else if (forcedTeleporter.transform.name == "HallwayDoor302")
            {
                SetHallwayVariables();
                transform.position = GetTeleportPosition(destination);
            }
            else if (forcedTeleporter.transform.name == "Door-304-friend")
            {
                SetRoomVariables(304);
                transform.position = GetTeleportPosition(destination);
            }
            else if (forcedTeleporter.transform.name == "HallwayDoor304")
            {
                SetHallwayVariables();
                transform.position = GetTeleportPosition(destination);
            }

        }
    }

    /* Allows teleport as a command to call in Yarn, to simplify some of the teleporting.
     * Usage: pass in the name of the door that you want to teleport from.*/

    /* TODO: smooth transitions for teleport? IENumerator? */

    [YarnCommand("teleport")]
    public void dialogue_teleport_from_door(string door)
    {
        Transform destination = GameObject.Find(door).GetComponent<Teleporter>().GetDestination();
        if (door == "Door302-FortuneTeller")
        {
            SetRoomVariables(302);
            transform.position = destination.position;
        }
        else if (door == "HallwayDoor302")
        {
            SetHallwayVariables();
            transform.position = GetTeleportPosition(destination);
        }
        else if (door == "Door-304-friend")
        {
            SetRoomVariables(304);
            transform.position = GetTeleportPosition(destination);
        }
        else if (door == "HallwayDoor304")
        {
            SetHallwayVariables();
            transform.position = GetTeleportPosition(destination);
        }
        else if (door == "Door-102-Mother")
        {
            SetRoomVariables(102);
            transform.position = GetTeleportPosition(destination);
        }

    }

    void SetRoomVariables(int roomNum)
    {
        move2D = true;
        movespeed = roomMoveSpeed;
        Debug.Log(gameState);
        gameState.inRoom = true;
        gameState.roomNum = roomNum;
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
            anim.SetFloat("Speed", 1);
            sr.flipX = true;
        }
        else if (x_input < 0)
        {
            anim.SetFloat("Speed", 1);
            PlayerRB.velocity = Vector2.left * movespeed;
            currDirection = Vector2.left;
            sr.flipX = false;
        } else if (y_input > 0 && move2D)  //&& SceneManager.GetActiveScene().name != "HallwayLayout")
        {
            PlayerRB.velocity = Vector2.up * movespeed;
            currDirection = Vector2.up;
            anim.SetFloat("Speed", 1);
            sr.flipX = true;
        }
        else if (y_input < 0 && move2D)  //&& SceneManager.GetActiveScene().name != "HallwayLayout")
        {
            anim.SetFloat("Speed", 1);
            PlayerRB.velocity = Vector2.down * movespeed;
            currDirection = Vector2.down;
            sr.flipX = true;
        }
        else
        {
            anim.SetFloat("Speed", 0);
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
        if (coll.CompareTag("DoorTeleporter"))
        {
            doorTeleporter = coll.gameObject;
        }
        if (coll.CompareTag("ForcedTeleporter"))
        {
            forcedTeleporter = coll.gameObject;
        }
        if (coll.CompareTag("Stair"))
        {
            stairTeleporter = coll.gameObject;
        }
        if (coll.CompareTag("DogDoor"))
        {
            inFrontDogHome = true;
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("DoorTeleporter"))
        {
            if (coll.gameObject == doorTeleporter)
            {
                doorTeleporter = null;
            }
        }
        if (coll.CompareTag("ForcedTeleporter"))
        {
            if (coll.gameObject == forcedTeleporter)
            {
                forcedTeleporter = null;
            }
        }
        if (coll.CompareTag("Stair"))
        {
            if (coll.gameObject == stairTeleporter)
            {
                stairTeleporter = null;
            }
        }
        if (coll.CompareTag("DogDoor"))
        {
            if (inFrontDogHome)
            {
                inFrontDogHome = false;
            }
        }
    } 

    public IEnumerable Teleport()
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
        return colliders.bounds.center.y;// + (collider.size.y / 2);// - collider.offset.y;
    }
    #endregion


}

