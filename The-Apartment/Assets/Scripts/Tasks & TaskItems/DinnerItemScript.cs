using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinnerItemScript : MonoBehaviour
{

    private GameObject task;
    public int stage;
    private Vector3 offset;

    #region Sprites
    public GameObject oven;
    public Sprite ovenInProgress;
    public Sprite ovenDone;
    #endregion

    void Start()
    {
        task = this.transform.parent.gameObject;
    }

    void OnMouseDown()
    {
        Debug.Log("Dinner item clicked");
      
        MakeDinnerTask dinnerTask = task.GetComponent<MakeDinnerTask>();
        Debug.Log(dinnerTask.stage + " " + this.stage);
        if (dinnerTask.stage == this.stage)
        {
            // special case for oven animation
            if (dinnerTask.stage == 3)
            {
                StartCoroutine(OvenAnimation());
                return;
            }


            dinnerTask.incrementProgress();
            Destroy(this.transform.gameObject);
        }
    }

    public IEnumerator OvenAnimation()
    {
        // change to in progress
        oven.GetComponent<SpriteRenderer>().sprite = ovenInProgress; // sprite
        yield return new WaitForSeconds(3f);
        oven.GetComponent<SpriteRenderer>().sprite = ovenDone; // sprite
        yield return new WaitForSeconds(0.5f);
        task.GetComponent<MakeDinnerTask>().incrementProgress();
        Destroy(this.transform.gameObject);
    }

}
