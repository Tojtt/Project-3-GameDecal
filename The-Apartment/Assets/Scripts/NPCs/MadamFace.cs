using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadamFace : MonoBehaviour
{
    // Start is called before the first frame update
    public float triggerDistance = 2.0f;
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        this.gameObject.GetComponent<Renderer>().enabled = (distance < triggerDistance);
    }
}
