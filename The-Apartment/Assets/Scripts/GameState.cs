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

    private void Start()
    {
        taskQueue = new List<GameObject>();
        PrepareNextDay(day);
    }

    #region Functions
    private void PrepareNextDay(int day)
    {
        taskQueue.Add(Instantiate(tasks[0]));
        dayFinished = false;
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
            CobwebTaskScript s = taskQueue[0].GetComponent<CobwebTaskScript>();
            while (s.isTaskComplete())
            {
                taskQueue.Remove(taskQueue[0]);
                if (taskQueue.Count > 0)
                {
                    s = taskQueue[0].GetComponent<CobwebTaskScript>();
                }
            }
        }
        //return curr task or none if none left
        if (taskQueue.Count > 0)
        {
            return taskQueue[0];
        }
        return null;
    }
    #endregion
}
