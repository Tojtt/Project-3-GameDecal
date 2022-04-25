using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnClothingTaskScript : AbstractTask
{
    #region Task_vars

    bool clothingAcquired;
    bool friendRoomEntered;
    string description;
    string taskname;

    #endregion


    public override void Awake()
    {
        clothingAcquired = false;
        friendRoomEntered = false;
        description = "Return your friend's forgotten coat.";
        taskname = "Return friend's forgotten coast";
    }

    public override string getProgessString()
    {
        if (!clothingAcquired)
        {
            return "Grab your Kenn's coat from your room.";
        }
        else
        {
            return "Return the coat to Kenn's room.";
        }
    }

    public override string getTaskName()
    {
        return taskname;
    }

    public override void incrementProgress()
    {
        if (!clothingAcquired)
        {
            clothingAcquired = true;
            //activate friend room entering
        }
        else
        {
            friendRoomEntered = true;
        }
    }

    public override bool isTaskComplete()
    {
        return clothingAcquired && friendRoomEntered;
    }

    public override void Start()
    {
        
    }

    public override void Update()
    {
        
    }
}
