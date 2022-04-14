using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

/* Script holding all the Yarn functions. */
public class YarnCommands : MonoBehaviour
{
    //TODO: Store references in beginning so not too expensive


    // Loads all Yarn functions at Start
    private void Start()
    {
        DialogueRunner dr = GetComponent<DialogueRunner>();
        // Adds a Yarn Command to generate a number [0, range)
        dr.AddFunction("random",
            (int range) => { return Random.Range(0, range); });

        // Retrieves the current day from GameState
        dr.AddFunction("getCurrentDay",
            () => { return GetComponent<GameState>().day;  });

        // Ends TV scene and loads the next day
        // TODO link with the current scene manager
        dr.AddFunction<bool>("loadNextDay",
            () => {
                int nextDay = GetComponent<GameState>().day;
                SceneManager.LoadScene("Hallway");
                return true;
            });

        dr.AddFunction("getTimesKnocked",
            () => {
                return GameObject.Find("RoomDoor302").GetComponent<MadamFace>().doorTimes - 1; });
    }

}
