using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    
    public Animator transition;
    public float transitionTime = 1f;
    // Start is called before the first frame update

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("ApartmentDoor"))
        {
            
            StartCoroutine(LoadLevel("Apartment"));
        }
    }
    

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Coming THROUGH!!");
            StartCoroutine(LoadLevel("Apartment"));
        }
    }

    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
