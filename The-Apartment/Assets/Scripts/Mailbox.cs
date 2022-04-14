using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mailbox : MonoBehaviour
{
    #region Variables
    bool opened = false;

    #endregion

    #region Unity_Functions
    void OnMouseClick()
    {
        if (!opened)
        {
            //Change sprite appearance
            opened = true;
        }
        else
        {
            //Change sprite appearance
            opened = false;
        }
    }
    #endregion
}
