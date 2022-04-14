using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject playerChar;
    GameObject friendChar;

    void Start()
    {
        playerChar = GameObject.Find("Player");
        friendChar = GameObject.Find("Friend");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
