using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup myUIGroup;
    public float time; //Coroutine Waiting Time
   
    public IEnumerator FadeIn()
    {
        while (myUIGroup.alpha < 1)
        {
            yield return myUIGroup.alpha += Time.deltaTime;
        }
         

    }
    public IEnumerator FadeOut()
    {
        while (myUIGroup.alpha >= 0)
        {
            yield return myUIGroup.alpha -= Time.deltaTime;
        }
    }
}
