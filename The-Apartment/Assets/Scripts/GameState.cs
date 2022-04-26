using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class GameState : MonoBehaviour
{

    public static GameState Instance;

    #region Possible Tasks for each day, chosen from randomly in the code. Remove this region when we have specific tasks for each day implemented!
    [SerializeField]
    GameObject[] tasks;
    [SerializeField]
    #endregion


    #region Tasks for each day.

    GameObject[] day1_tasks;
    [SerializeField]
    GameObject[] day2_tasks;
    [SerializeField]
    GameObject[] day3_tasks;
    [SerializeField]
    GameObject[] day4_tasks;
    [SerializeField]
    GameObject[] day5_tasks;
    [SerializeField]
    GameObject[] day6_tasks;
    [SerializeField]
    GameObject[] day7_tasks;

    #endregion


    #region GameState_Variables
    //Array of tasks for any given day d is stored at tasks_for_day[d - 1].
    private List<GameObject[]> tasks_for_day;
    // denotes whether all tasks are completed and can progress to the next day
    public bool dayFinished;
    // current day
    public int day;
    // current tasks active now
    public List<GameObject> taskQueue;
    // denotes whether the player has watched the TV
    public bool watchedTV;
    //Whether player is frozen
    public bool freezePlayer = false;
    // dialogue runner object
    public DialogueRunner dialogue;
    // friend gameobject
    public GameObject friend;
    // denotes whether the entrance is blocked - should be blocked Day 4 and onward
    public bool entranceBlocked = false;

    public float moneyEarned = 0;
    #endregion

    #region Location_Variables
    public int floor;// 0-Basement, 4-Outside
    public bool inRoom = false;
    public int roomNum = -1;
    #endregion

    #region Day2_Cutscene_Variables
    public float speed = 1.0f; // speed of friend moving
    public GameObject doorExit; // holds teleporter
    //public Transform target; // location where friend will move, used for cutscenes 
    #endregion 

    #region Day6_Variables
    public bool pouredGas;
   public bool fireExtinguisherStolen;
   public bool fireStarted;

   int fireFailedAttempts;
    #endregion

    #region TaskCompletion_Variables
    //Fortune Teller
    public bool fortuneTellingComplete = false;
    public bool lostDogComplete = false;
    #endregion

    #region Unity_Functions
    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        taskQueue = new List<GameObject>();
        tasks_for_day = new List<GameObject[]> {day1_tasks, day2_tasks, day3_tasks, day4_tasks, day5_tasks, day6_tasks, day7_tasks};
        //day = 1;
        watchedTV = false;
        PrepareNextDay(day);
        if (day == 6)
        {
            ResetDay6();
        }
        if (day < 4) //No need for friend after day 3
        {
            //friend.SetActive(false);
        }
    }

    void Update()
    {
        if (day == 6)
        {
            if (true)
            {

            }
            else if (false) //End of day timer
            {
                //Ending 0 Cutscene
            }
            else if (false) //End of fire timer
            {
                //Ending 5 cutscene
            }
        }
        
    }
    #endregion

    #region Daily_Functions
    public void PrepareNextDay(int day)
    {
        if (day < 6)
        {
            taskQueue = new List<GameObject>();

            Debug.Log("Preparing day");
            Debug.Log("Current day: " + (day - 1));
            foreach (GameObject task in tasks_for_day[day - 1])
            {
                taskQueue.Add(Instantiate(task));
                Debug.Log(task.name);
            }
            dayFinished = false;
            taskQueue[0].SetActive(true);

            Debug.Log(taskQueue.Count);
        }

    }

    /* The player sleeps and this function is called to advance the day. */
    public void nextDay()
    {
        Debug.Log("Advancing next day");
        if (dayFinished)
        {
            day += 1;

            GameManager.Instance.nightTransition();
            SceneManager.LoadScene("Day" + day + "Scene");
            PrepareNextDay(day);
        }
    }

    public GameObject getCurrentTask()
    {
        //remove finished tasks 
        if (taskQueue.Count > 0)
        {
            AbstractTask s = taskQueue[0].GetComponent<AbstractTask>();
            while (s.isTaskComplete())
            {
                taskQueue.Remove(taskQueue[0]);
                if (taskQueue.Count > 0)
                {
                    s = taskQueue[0].GetComponent<AbstractTask>();
                }
            }
        }
        //return curr task or none if none left
        if (taskQueue.Count > 0)
        {
            taskQueue[0].SetActive(true);
            return taskQueue[0];
        }
        else {
            dayFinished = true;
            return null;
        }
    }

    public bool isDayFinished()
    {
        return dayFinished;
    }
    #endregion

    #region Day2_Functions 
    public void RunFriendDinner()
    {
        // Friend walks in - active
        FindObjectOfType<Bed>().disabled = true;

        Debug.Log("Run friend dinner cutscene");
        StartCoroutine(MoveFriend());
        // Dialogue runs
        if (!dialogue.IsDialogueRunning)
        {
            dialogue.StartDialogue("friendCutscene");
            // when dialogue is finished, have friend disappear
            dialogue.onDialogueComplete.AddListener(DisableFriend);
            //friend.SetActive(false);
        }

        // Cutscene ends
        
    }

    public IEnumerator MoveFriend()
    {
        doorExit = GameObject.Find("HallwayDoor206");
        doorExit.SetActive(false);
        friend.SetActive(true);
        float step = speed * Time.deltaTime;
        // move towards these coordinates
        Vector3 target = new Vector3(75.14f, 1.41f);
        //friend.transform.position = Vector3.MoveTowards(transform.position, target, step);
        friend.transform.position = target;

        // pause
        yield return new WaitForSeconds(0.5f);
        //friend.SetActive(false);
        FindObjectOfType<Bed>().disabled = false;

    }

    public void DisableFriend()
    {
        friend.SetActive(false);
        doorExit.SetActive(true);
        
    }
    #endregion

    #region Day6_Functions
    void ResetDay6()
    {
        pouredGas = false;
        fireExtinguisherStolen = false;
        fireStarted = false;
        fireFailedAttempts = 0;
    }

    void EndDay6() //<<<<<<Call this function when reached outside
    {
        //Assuming we are outside
        if (fireStarted)
        {
            //Ending 1 cutscene
            
        }
        
    }

    void StartFire()
    {
        if (true) //Not near gasoline spill
        {
            if (fireFailedAttempts == 3)
            {
                //Ending 3 Cutscene
            } else
            {
                //Fire sizzles out cutscene
                fireFailedAttempts++;
            }
        } else if (!fireExtinguisherStolen) //Ending 2
        {
            //Begin fire animation only for a few seconds
            fireStarted = true;
            //Ending 2 Cutscene 
        } else
        {
            //Begin fire animation
            fireStarted = true;
        }
    }

    /**List of Day 6 Endings:
    Ending 0) Still in building/not started fire by the end of dayTimer ->die
    Ending 1) Success -> All tasks completed
    Ending 2) Did not steal fire extinguisher, but started fire
    Ending 3) Tried to start fire 3 times without gasoline
    Ending 4) Started fire but timer ran out and you did not escape by end of fireTimer -> burn
    //Add on
    Ending 5) You spill gas in front of NPC -> they like what u doing
    **/
    #endregion
}