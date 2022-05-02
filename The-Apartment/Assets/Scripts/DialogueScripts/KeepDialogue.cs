using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepDialogue : MonoBehaviour
{
    public static KeepDialogue Instance;

    // Start is called before the first frame update
    void Start()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
