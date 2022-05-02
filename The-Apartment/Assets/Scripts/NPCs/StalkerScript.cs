using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerScript : MonoBehaviour
{
    [SerializeField]
    int movespeed;
    [SerializeField]
    float walkDelay;
    [SerializeField]
    float retreatRange;
    [SerializeField]
    float retreatSpeed;
    private bool retreating;
    private float currTimer;
    private GameObject player;
    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        currTimer = 0;
        player = GameObject.Find("Player");
        rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        retreating = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toPlayer = player.transform.position - this.gameObject.transform.position;
        if (toPlayer.magnitude < retreatRange && !retreating)
        {
            currTimer = 0;
            retreating = true;
            StartCoroutine(moveAwayFromPlayer());
        }
        else if (currTimer > walkDelay)
        {
            currTimer = 0;
            StartCoroutine(moveTowardPlayer());
        }
        currTimer += Time.deltaTime;
    }

    IEnumerator moveTowardPlayer()
    {
        Vector3 toPlayer = player.transform.position - this.gameObject.transform.position;
        Vector2 movementVector = new Vector2(toPlayer.normalized[0], 0);
        rigidBody.velocity = movementVector * movespeed/3;
        yield return new WaitForSeconds(1f);
        rigidBody.velocity = new Vector2(0, 0);
    }

    IEnumerator moveAwayFromPlayer()
    {
        Vector3 toPlayer = player.transform.position - this.gameObject.transform.position;
        Vector2 movementVector = new Vector2(-1 * toPlayer.normalized[0], 0);
        rigidBody.velocity = movementVector * retreatSpeed;
        yield return new WaitForSeconds(1f);
        rigidBody.velocity = new Vector2(0, 0);
        retreating = false;
    }
}
