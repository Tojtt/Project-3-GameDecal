using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoudNeighbor : MonoBehaviour
{
    [SerializeField]
    int floatSpeed;
    [SerializeField]
    float appearRange;
    private bool activeMovement;
    private float currTimer;
    private GameObject player;
    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        currTimer = 0;
        player = GameObject.Find("Player");
        rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        activeMovement = false;
    }

    // Update is called once per frame
    void Update()
    {
        float toPlayer = Vector3.Distance(player.transform.position,this.gameObject.transform.position);
        if (toPlayer >= appearRange)
        {
            currTimer = 0;
            activeMovement = true;
            StartCoroutine(floatingAround());
        }
        else if (toPlayer < appearRange)
        {
            currTimer = 0;
            //Disappear();
        }
        currTimer += Time.deltaTime;
    }

    IEnumerator floatingAround()
    {
        // Vector3 toPlayer = player.transform.position - this.gameObject.transform.position;
        // Vector2 movementVector = new Vector2(toPlayer.normalized[0], 0);
        // rigidBody.velocity = movementVector * movespeed/3;
        yield return new WaitForSeconds(1f);
        // rigidBody.velocity = new Vector2(0, 0);
    }

    IEnumerator moveAwayFromPlayer()
    {
        // Vector3 toPlayer = player.transform.position - this.gameObject.transform.position;
        // Vector2 movementVector = new Vector2(-1 * toPlayer.normalized[0], 0);
        // rigidBody.velocity = movementVector * movespeed;
        yield return new WaitForSeconds(1f);
        // rigidBody.velocity = new Vector2(0, 0);
        // retreating = false;
    }
}
