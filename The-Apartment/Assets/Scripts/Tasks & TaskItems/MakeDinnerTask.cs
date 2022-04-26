using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDinnerTask : AbstractTask
{
    #region Progress_Variables
    private bool isComplete;
    private string description;
    private string stageDesc;
    private int totalStages;
    public int stage;
    [SerializeField]
    private string taskName;
    #endregion

    #region AbstractTask_Funcs

    public override void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    public override void Start()
    {
        isComplete = false;
        stage = 0;
        description = "Make dinner for your friend!\n" + getStageDesc(stage);
        totalStages = 4;
        taskName = "Dinner";
        

    }

    public override string getProgessString()
    {
        return getStageDesc(stage);
    }

    public override void incrementProgress()
    {
        stage += 1;
        description = "Make dinner for your friend!\n" + getStageDesc(stage);
        if (stage == totalStages)
        {

            // run the Dinner cutscene before setting as complete
            GameState.Instance.RunFriendDinner();

            isComplete = true; 
        }
    }

    public override void Update()
    {
        
    }

    public override string getTaskName()
    {
        return taskName;
    }

    public override bool isTaskComplete()
    {
        return isComplete;
    }

    #endregion

    #region Day2_Functions 
    /* public void RunFriendDinner()
    {
        // Friend walks in - active
        FindObjectOfType<Bed>().disabled = true;
        friend.SetActive(true);

        Debug.Log("Run friend dinner cutscene");
        StartCoroutine(MoveFriend());
        // Dialogue runs
        if (!dialogue.IsDialogueRunning)
        {
            dialogue.StartDialogue("friendCutscene");
        }

        // Cutscene ends 
    }

    public IEnumerator MoveFriend()
    {
        float step = speed * Time.deltaTime;
        target.position = new Vector3(32.07f, 5.38f);
        friend.transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        yield return new WaitForSeconds(0.5f);
        friend.SetActive(false);

    } */
    #endregion

    private string getStageDesc(int stage)
    {
        switch (stage)
        {
            case 0:
                // code block
                return "Get the pizza dough";
            case 1:
                // code block
                return "Add in the tomato sauce";
            case 2:
                return "Add in the cheese";
            case 3:
                return "Put the pizza in the oven";
            default:
                // code block
                return "Dinner is finished!";
        }
    }



}
