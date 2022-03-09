using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    #region GameState_Variables
    // denotes whether all tasks are completed and can progress to the next day
    public bool dayFinished;
    // current day
    public int day;
    #endregion

    private void Start()
    {
        dayFinished = false;
        day = 0;
    }

    #region Functions
    private void PrepareNextDay(int day)
    {

        // at the end
        dayFinished = false;
    }

    private void CheckIfFinished()
    {

    }
    #endregion
}
