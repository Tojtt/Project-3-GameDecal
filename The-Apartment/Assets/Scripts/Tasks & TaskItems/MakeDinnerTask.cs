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

    #region Unity_Variables
    public GameObject pizza;
    public GameObject oven;
    [SerializeField]
    public Sprite[] stages;
    [SerializeField]
    public Sprite ovenInProgress;
    public Sprite ovenDone;
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

        oven = GameObject.Find("Oven");

    }

    public override string getProgessString()
    {
        return getStageDesc(stage);
    }

    public override void incrementProgress()
    {
        // putting pizza in the oven
        if (stage == 3)
        {
            StartCoroutine(OvenAnimation());
        }

        stage += 1;
        description = "Make dinner for your friend!\n" + getStageDesc(stage);

        if (stage == totalStages)
        {

            // run the Dinner cutscene before setting as complete
            GameState.Instance.RunFriendDinner();
            // remove pizza from scene 
            pizza.SetActive(false);

            isComplete = true; 
        }
    }

    public override void Update()
    {
        pizza.GetComponent<SpriteRenderer>().sprite = stages[stage-1];
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

    public IEnumerator OvenAnimation()
    {
        // change to in progress
        oven.GetComponent<SpriteRenderer>().sprite = ovenInProgress; // sprite
        yield return new WaitForSeconds(3f);
        oven.GetComponent<SpriteRenderer>().sprite = ovenDone; // sprite

    }

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
