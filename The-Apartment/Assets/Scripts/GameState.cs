using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    #endregion

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
        day = 1;
        PrepareNextDay(day);
    }

    #region Functions
    private void PrepareNextDay(int day)
    {
        //old code
        /**if (day < 6)
        {
            taskQueue.Add(Instantiate(tasks[0]));
            taskQueue.Add(Instantiate(tasks[1]));
            dayFinished = false;
            Debug.Log(taskQueue.Count);
        }*/
        taskQueue = new List<GameObject>();

        Debug.Log("Preparing day");

        foreach (GameObject task in tasks_for_day[day - 1])
        {
            taskQueue.Add(Instantiate(task));
            Debug.Log(task.name);
        }
        dayFinished = false;
        taskQueue[0].SetActive(true);

        Debug.Log(taskQueue.Count);

    }

    /* The player sleeps and this function is called to advance the day. */
    public void nextDay()
    {
        Debug.Log("Advancing next day");
        if (dayFinished)
        {
            day += 1;
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

}
