using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class LineOfSight : MonoBehaviour
{

    #region Unity_Variables
    public YarnInteractable yarn;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit collider");
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Hit collider");
            yarn.StartConversation();
        }
        
    }
}
