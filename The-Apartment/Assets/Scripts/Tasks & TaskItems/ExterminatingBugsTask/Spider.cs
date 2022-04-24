using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{

    #region Variables
    public float elapsedTime;
    [SerializeField]
    [Tooltip("radius of enemy position")]
    private float radius;
    public Vector2 startingPosition;
    // gameobject for the sink closeup
    public GameObject sinkOverlay;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        sinkOverlay = GameObject.Find("SinkOverlay");
        float x = sinkOverlay.transform.position.x;
        float y = sinkOverlay.transform.position.y;
        float offset = 10f;
        transform.position = new Vector2(
            Random.Range(x - offset, x + offset),
            Random.Range(y - offset, y + offset));
        Debug.Log("Position of spider: " + transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startingPosition + new
            Vector2(radius * Mathf.Sin(elapsedTime), radius *
            Mathf.Cos(elapsedTime));
        elapsedTime = elapsedTime + Time.deltaTime;
    }
}
