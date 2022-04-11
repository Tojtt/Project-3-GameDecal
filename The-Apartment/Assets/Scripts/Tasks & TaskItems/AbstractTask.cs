using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTask : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void Start();

    // Start is called before the first frame update
    public abstract void Awake();

    // Update is called once per frame
    public abstract void Update();

    public abstract string getTaskName();

    public abstract void incrementProgress();

    public abstract string getProgessString();

    public abstract bool isTaskComplete();
}
