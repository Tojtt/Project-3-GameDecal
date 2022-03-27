using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    #region Editor_variables
    public float clickRadius;
    public int doorsPerFloor;
    #endregion

    #region Non_editor_variables
    private int numFloors = 3;
    
    private GameObject[] doors; //List of door GameObjects
    GameObject player;
    List<string>[] doorDialogues;
    
    #endregion

    #region Unity_functions
    void Awake()
    {
        player = GameObject.Find("Player"); 
        doors = new GameObject[numFloors * doorsPerFloor];
        
        //Initialize list of dialogues for each door (hard code)
        doorDialogues = new List<string>[3];
        doorDialogues[0] = new List<string> { "hi", "bye", "stop" };
        doorDialogues[1] = new List<string> { "who's there", "ok", "grr" };
        doorDialogues[2] = new List<string> { "..." };

        //Store all the door gameObjects -> doors
        for (int i = 0; i < numFloors; i++)
        {
            for (int j = 0; j < doorsPerFloor; j++)
            {
                int index = i * doorsPerFloor + j;
                
                string roomNum = ((i + 1) * 100 + (j + 1)).ToString();
                doors[index] = GameObject.Find("Door" + roomNum);
            }
        }
        Debug.Log(doors);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Spawn_functions
    //Spawn all doors on all floors in their correct locations
    void SpawnDoors()
    {

    }
    #endregion

    #region Dialogue_functions
    public string GetDialogue(int dialogue_id, int clickCount)
    {
        Debug.Log(doorDialogues);
        int i = System.Math.Min(clickCount - 1, doorDialogues[dialogue_id].Count - 1);
        return doorDialogues[dialogue_id][i];
    }
    #endregion
    #region Range_functions
    public bool InClickRange(Vector3 doorPosition)
    {
        Debug.Log(DistanceToPlayer(doorPosition));
        return  DistanceToPlayer(doorPosition) < clickRadius;
    }


    float DistanceToPlayer(Vector2 doorPosition)
    {
        //X position only distance
        return Mathf.Abs(player.transform.position.x - doorPosition.x);
        //return Vector2.Distance(player.transform.position, doorPosition);
    }
    #endregion


}
