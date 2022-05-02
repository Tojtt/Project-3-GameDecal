using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class SceneTransitions : MonoBehaviour
{
    public Animator transitionAnim;
    public GameState gs;
    public DialogueRunner dialogue;
    public bool runDialogueBefore = false;

    void Update(){
        // if(Input.GetKeyDown(KeyCode.M)){
        //     StartCoroutine(LoadScene(nextscene));
        //}
    }


    public IEnumerator LoadScene(string sceneName){
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);

    }


    public IEnumerator TeleportTransition()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        transitionAnim.SetTrigger("start");

    }


    public void OnMouseDown()
    {
        Debug.Log("Clicked");
        string scene = getScene();
        Debug.Log(scene);
        StartCoroutine(LoadScene(scene));
    }

    private string getScene()
    {
        return "Day" + GameState.Instance.day + "Scene";
    }
}