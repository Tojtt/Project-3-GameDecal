using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TauntingNPC : MonoBehaviour
{
    public YarnInteractable yarnScript;

    private void Awake()
    {
        yarnScript = FindObjectOfType<YarnInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}
