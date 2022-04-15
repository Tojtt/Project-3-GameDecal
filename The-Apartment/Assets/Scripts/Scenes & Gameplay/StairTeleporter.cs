using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairTeleporter : MonoBehaviour
{
    [SerializeField] private Transform upDestination;
    [SerializeField] private Transform downDestination;

    public Transform GetUpDestination()
    {
        return upDestination;
    }

    public Transform GetDownDestination()
    {
        return downDestination;
    }
}
