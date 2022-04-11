using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    #region Possible Tasks for each day
    [SerializeField]
    GameObject[] tasks;
    #endregion

    
    #region GameState_Variables
    // denotes whether all tasks are completed and can progress to the next day
    public bool dayFinished;
    // current day
    public int day;
    private List<GameObject> taskQueue;
    #endregion

    #region Day6_Variables
    public bool pouredGas;
    bool entranceChainedUp;
    bool fireExtinguisherStolen;
    bool fireStarted;

    int noGasAttempts;
    #endregion

    #region Unity_Functions
    private void Start()
    {
        taskQueue = new List<GameObject>();
        PrepareNextDay(day);
        if (day == 6)
        {
            ResetDay6();
        }
    }

    void Update()
    {
        if (false) //End of day timer
        {
            //Ending 0 Cutscene
        } else if (false) //End of fire timer
        {
            //Ending 5 cutscene
        }
    }
    #endregion

    #region Daily_Functions
    private void PrepareNextDay(int day)
    {
        if (day < 6)
        {
            taskQueue.Add(Instantiate(tasks[0]));
            taskQueue.Add(Instantiate(tasks[1]));
            dayFinished = false;
            Debug.Log(taskQueue.Count);
        }
    }

    /* The player sleeps and this function is called to advance the day. */
    public void nextDay()
    {
        if (taskQueue.Count == 0)
        {
            day += 1;
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
    #endregion

    #region Day6_Functions
    void ResetDay6()
    {
        pouredGas = false;
        entranceChainedUp = false;
        fireExtinguisherStolen = false;
        fireStarted = false;
        noGasAttempts = 0;
    }

    void EndDay6() //<<<<<<Call this function when reached outside
    {
        //Assuming we are outside
        if (fireStarted)
        {
            if (!entranceChainedUp) //Ending 4
            {
                //Ending 4 cutscene
            }
            else //Ending 1 cutscene
            {
                //Ending 1 cutscene
            }
        }
        
    }

    void StartFire()
    {
        if (true) //Not near gasoline spill
        {
            if (noGasAttempts == 3)
            {
                //Ending 3 Cutscene
            } else
            {
                //Fire sizzles out cutscene
                noGasAttempts++;
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
    Ending 0) Still in building by the end of dayTimer ->die
    Ending 1) Success -> All tasks completed
    Ending 2) Did not steal fire extinguisher, but started fire
    Ending 3) Tried to start fire 3 times without gasoline
    Ending 4) Started fire but ending not chained up 
    Ending 5) Started fire but timer ran out and you did not escape by end of fireTimer -> burn
    //Add on
    Ending 6) You spill gas in front of NPC -> they like what u doing
    **/
    #endregion
}
