using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("trying to sleep");
        Debug.Log("day finished");
        Debug.Log(GameState.Instance.dayFinished);
        if (GameState.Instance.dayFinished)
        {
            GameState.Instance.nextDay();
        }
    }
}
