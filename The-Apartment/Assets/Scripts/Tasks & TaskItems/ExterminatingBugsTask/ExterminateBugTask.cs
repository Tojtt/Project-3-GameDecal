using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ExterminateBugTask : AbstractTask
{
    #region Progress_Variables
    private bool isComplete; // denotes completion of task
    private string description; // denotes description of task
    public bool isActive;
    public int spidersKilled;
    #endregion

    #region Minigame_Configs
    public int numSpiders;
    public float speed = 5.0f;
    int numSpidersToComplete = 10;
    #endregion 

    #region Unity_Variables
    public DialogueRunner dialogueRunner;
    public string startNode;
    #endregion

    #region Editor Variables
    [SerializeField]
    [Tooltip("The different types of enemies that should be spawned and their corresponding spawn information.")]
    private EnemySpawnInfo[] m_EnemyTypes;
    public bool enabled = false;
    Vector3 startPosition;
    #endregion
    GameState gameState;

    #region AbstractTask_Functions
    public override void Awake()
    {
        //throw new System.NotImplementedException();
    }

    public override void Start()
    {
        startPosition = new Vector3(-112.64f, 18.058f, 0);
        isComplete = false;
        isActive = true;
        spidersKilled = 0;
        description = "Check your sink...";
        //dialogueRunner.Stop();
        //if (!dialogueRunner.IsDialogueRunning)
        //{
        //    dialogueRunner.StartDialogue(startNode);
        //}
        gameState = GameObject.FindWithTag("GameManager").GetComponent<GameState>();

        Debug.Log("done");
    }

    public override void Update()
    {
        if (isComplete)
        {
            isActive = false;
        }

        
    }

    public override string getProgessString()
    {
        return "Spiders squashed: " + spidersKilled + "/" + numSpiders;
    }

    public override string getTaskName()
    {
        return "Exterminating Bugs";
    }

    public override void incrementProgress()
    {
        // if spider killed
        spidersKilled += 1;

        if (spidersKilled == numSpidersToComplete)
        {
            isComplete = true;
            gameState.spidersComplete = true;
            Debug.Log("Spider task done");
        }
    }

    public override bool isTaskComplete()
    {
        return isComplete;
    }

    #endregion

    #region Cutscene_Functions
    public void Spawn()
    {
        StartCoroutine(SpawnSpiders());
    }

    IEnumerator SpawnSpiders()
    {
        //GetComponent<SpawnManager>().enabled = true;
        for (int i = 0; i < numSpiders; i++)
        {
            EnemySpawnInfo enemy = m_EnemyTypes[0];
            Instantiate(enemy.EnemyPrefab, startPosition, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }
    #endregion

    #region Enemy_Info
    [System.Serializable]
    public struct EnemySpawnInfo
    {
        #region Editor Variables
        [SerializeField]
        [Tooltip("The enemy prefab to spawn. This is what will be instantiated each time.")]
        private GameObject m_EnemyPrefab;

        [SerializeField]
        [Tooltip("The time we should wait before the first enemy is spawned.")]
        private float m_FirstSpawnTime;

        [SerializeField]
        [Range(0, 100)]
        [Tooltip("How many enemies should spawn per second.")]
        private float m_SpawnRate;
        #endregion

        #region Accessors and Mutators
        public GameObject EnemyPrefab
        {
            get { return m_EnemyPrefab; }
        }

        public float FirstSpawnTime
        {
            get { return m_FirstSpawnTime; }
        }

        // Doing (1 / SpawnRate) might be more useful than directly using SpawnRate
        public float SpawnRate
        {
            get { return m_SpawnRate; }
        }
        #endregion
    }
    #endregion

}
