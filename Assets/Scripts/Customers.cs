using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customers : MonoBehaviour
{
    public static Customers Instance;
    public List<Transform> spawnpoints = new();
    public Transform startingPoint;
    public Transform finishingPoint;
    public GameObject currentCustomer;

    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        GenerateCustomer();
    }

    public void GenerateCustomer()
    {
        var startingIdx = Random.Range(0, spawnpoints.Count - 1);
        var finishingIdx = Random.Range(0, spawnpoints.Count - 1);

        while(finishingIdx == startingIdx)
           finishingIdx = Random.Range(0, spawnpoints.Count - 1);

        startingPoint = spawnpoints[startingIdx];
        finishingPoint = spawnpoints[finishingIdx];

        currentCustomer.transform.position = startingPoint.position;
    }

    public void PickUpCustomer()
    {
        startingPoint = null;
        currentCustomer.transform.position = new Vector3(0, -10000, 0);
    }

}