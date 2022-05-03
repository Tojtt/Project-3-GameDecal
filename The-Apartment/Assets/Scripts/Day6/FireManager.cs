using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public GameObject redGlobalLight;
    public GameObject normalGlobalLight;
    public GameObject playerLight;
    public SceneTransitions sceneTransition;

    public GameObject bus;

    int busStopX = -22;
    bool endStarted = false;
    GameState gameState;
    GameObject player;
    int floor;
    bool startedFire = false;
    void Awake()
    {
        player = GameObject.Find("Player");
        gameState = GameObject.FindWithTag("GameManager").GetComponent<GameState>();
        floor = gameState.floor;
        //redGlobalLight = GameObject.Find("GlobalLight2D-REDHOTFIRE");
        // playerLight = GameObject.Find("GlobalLight-PlayerOnly");
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (gameState.floor != floor)
        {
            floor = gameState.floor;
            if (floor == 4)
            {
                //StartCoroutine(sceneTransition.T1());
                //yield return new WaitForSeconds(1f);
                redGlobalLight.SetActive(false);
                normalGlobalLight.SetActive(true);
                
                //TurnOffHallwayLights();

                //  playerLight.SetActive(true);
                //foreach (Transform child in transform)
                //{
                //    child.gameObject.SetActive(true);
                //}
            } else
            {
                normalGlobalLight.SetActive(false);
                redGlobalLight.SetActive(true);
            }
        }
        if (!endStarted)
        {
            if (startedFire && gameState.floor == 4) //Outside
            {
                if (player.transform.position.x > busStopX)
                {
                    //End game - SUCCESS
                    gameState.freezePlayer = true;
                    endStarted = true;
                    StartCoroutine(EndWin());
                }
            }
        }
    }

    IEnumerator EndWin()
    {
        //Bus drives up
        Vector3 p = player.transform.position;
        while (bus.transform.position.x > busStopX)
        {
            Vector3 pos = bus.transform.position;
            
            bus.transform.position = new Vector3(pos.x - 0.5f, p.y, 0);
            yield return new WaitForSeconds(0.05f);
        }
        for (int i = 1; i < 6; i++)
        {
            Vector3 pos = bus.transform.position;
            bus.transform.position = new Vector3(pos.x - 0.5f / i, p.y, 0);
            yield return new WaitForSeconds(0.07f);
        }
        //Pause
        yield return new WaitForSeconds(3f);
        player.SetActive(false);
        //Drive away
        bus.GetComponent<SpriteRenderer>().flipX = true;
        for (int i = 1; i < 6; i++)
        {
            Vector3 pos = bus.transform.position;
            bus.transform.position = new Vector3(pos.x + 0.5f / (6- i), p.y, 0);
            yield return new WaitForSeconds(0.07f);
        }
        
        while (bus.transform.position.x < 0f)
        {
            Vector3 pos = bus.transform.position;
            bus.transform.position = new Vector3(pos.x + 0.5f, p.y, 0);
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(sceneTransition.LoadScene("EndSceneWin"));
        //Load end scene
    }

    void Start()
    { 
        StartFire();
    }

    #region Fire_functions
    public void StartFire()
    {
        startedFire = true;
        StartCoroutine(beginFire());
    }

    void TurnOffHallwayLights()
    {
        GameObject lights = GameObject.Find("HallwayLights");

        foreach (Transform child in lights.transform)
        {
            GameObject l = child.gameObject;
            l.GetComponent<LightScript>().turnOff();
        }
    }
    #endregion

    IEnumerator beginFire()
    {
        StartCoroutine(sceneTransition.T1());
        yield return new WaitForSeconds(1f);
        normalGlobalLight.SetActive(false);
        redGlobalLight.SetActive(true);
        TurnOffHallwayLights();

        //  playerLight.SetActive(true);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        StartCoroutine(sceneTransition.T2());
    }
}
