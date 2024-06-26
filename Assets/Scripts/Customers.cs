using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Customers : MonoBehaviour
{
    public static Customers Instance;
    public List<Transform> spawnpoints = new();
    public Transform startingPoint;
    public Transform finishingPoint;
    public GameObject currentCustomer;
    public int points = 0;
    public int potentialPoints;
    public TextMeshProUGUI pointsText;


    private void OnEnable()
    {
        CarController.onCustomerPickup += PickUpCustomer;
    }

    private void OnDisable()
    {
        CarController.onCustomerPickup -= PickUpCustomer;
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        GenerateCustomer();
        pointsText.text = "SCORE: 0";
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
        
        potentialPoints = (int)Vector3.Distance(startingPoint.position, finishingPoint.position);   
    }

    public void PickUpCustomer()
    {
        if (startingPoint == null)
        {
            Destroy(finishingPoint.GetComponent<SphereCollider>());
            finishingPoint.GetComponent<MeshRenderer>().enabled = false;
            points += potentialPoints;
            pointsText.text = $"SCORE: {points}";
            GenerateCustomer();
            return;
        }

        startingPoint = null;
        currentCustomer.transform.position = new Vector3(0, -10000, 0);
        finishingPoint.GetComponent<MeshRenderer>().enabled = true;
        var coll = finishingPoint.AddComponent<SphereCollider>();
        coll.radius = 4;
        coll.isTrigger = true;
    }
}