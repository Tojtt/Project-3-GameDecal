using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GasManager : MonoBehaviour
{
    #region Gas_variables
    [SerializeField]
    [Tooltip("The spilledGas prefab")]
    private GameObject m_spilledGas;

    private int maxDrops = 30;
    private int numDrops = 0;

    private float spill_y = -2.2f;
    private float y_off = 0.08f;
    private double x_offset = 1; //Spill x-offset from player

    private bool gasTankSelected;

    bool startedSpilling = false;
    bool doneSpilling = false;
    float spillTimer = 0;
    float spillWait = 0.2f;

    double minX;
    double maxX;
    double radius = 0.5;

    InventoryManager invM;
    #endregion

    #region spillLocation_Variables
    float basement_spill_y; //just pretend NPC's are in basement
    float f1_spill_y;
    float f2_spill_y;
    float f3_spill_y;
    float f1_minX;
    float f1_maxX;
    float f2_minX;
    float f2_maxX;
    float f3_minX;
    float f3_maxX;
    #endregion

    #region Referenced_Objects
    //GameObject player;
    PlayerController player;
    GameState gameState;
    FireManager fireManager;
    #endregion

    #region Unity_functions
    void Awake()
    {
        invM = GameObject.FindWithTag("Inventory").GetComponent<InventoryManager>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        gameState = GameObject.FindWithTag("GameManager").GetComponent<GameState>();
        fireManager = GameObject.Find("FireManager").GetComponent<FireManager>();
        gasTankSelected = true;
        GameObject.Find("FireManager").SetActive(false);
    }

    void Update()
    {
        if (!doneSpilling)
        {
            if (spillTimer > 0)
            {
                spillTimer -= Time.deltaTime;
            }

            if (startedSpilling)
            {
                if (spillTimer <= 0 && numDrops < maxDrops && gasTankSelected)
                {
                    PourGas();
                }
            }
        }
    }
    #endregion

    #region Fire_functions
    public void StartFire(bool onGas)
    {
        if (gameState.pouredGas && onGas)
        {
            Debug.Log("Successfully Started Fire");
            fireManager.gameObject.SetActive(true);
            gameState.fireStarted = true;
            fireManager.StartFire();
        } else
        {
            Debug.Log("Attempts to start fire, fire sizzles out");
            gameState.fireFailedAttempts += 1;
            if (gameState.fireFailedAttempts == 3)
            {
                //Ending 3 Cutscene
            }
        }
    }

    #endregion
    #region Pour_functions
    public void BeginSpilling()
    {
        if (!startedSpilling) {
           startedSpilling = true;
        }
        
    }

    //void OnMouseDown()
    //{
    //    if (!startedSpilling)
    //    {
    //        startedSpilling = true;
    //    }

    //}

    public void PourGas()
    {
        //Instantiate SpilledGas object at location
        double player_x = player.transform.position.x;
        double spill_x = (player.GetDirection() == Vector2.left) ? player_x - x_offset : player_x + x_offset;

        

        if (numDrops == 0 || (player_x < minX || player_x > maxX)) //Successful spill
        {
            //Debug.Log("spill");
            Vector3 spillPosition = new Vector3((float)spill_x, spill_y + -0.1f + Random.Range(-y_off, y_off), 0);
            Instantiate(m_spilledGas, spillPosition, Quaternion.identity);
           // Debug.Log("Poured gas");
            
            if (numDrops == 0)
            {
                maxX = player_x + radius;
                minX = player_x - radius;
            }
            else
            {
                minX = Mathf.Min((float)minX, (float)(player_x - radius));
                maxX = Mathf.Max((float)maxX, (float)(player_x + radius));
            }

            numDrops++;
            if (numDrops == maxDrops)
            {
                //Gas Tank is Empty
                doneSpilling = true;
                gameState.pouredGas = true;
                Debug.Log("Finish spilling gas!");
                invM.SetItemState(1, "Empty");
                invM.RemoveItem(1, true);
                
            }
            spillTimer = spillWait;
        }
    }

    public void ToggleGasTankSelected()
    {
        gasTankSelected = !gasTankSelected; 
    }
    #endregion
}
