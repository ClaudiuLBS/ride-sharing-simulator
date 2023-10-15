using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics : MonoBehaviour
{
    [SerializeField] private GameObject[] frontWheels;
    [SerializeField] private GameObject[] rearWheels;

    [Header("Wheels Configuration")]
    public GameObject wheelPrefab;
    public float wheelHeight;
    [Range(20, 60)] public float maxWheelsTilt = 40;
    public float tiltSmoothness = .5f;
    
    [Header("Suspension Settings")]
    public float suspensionRestDistance = .5f;
    public float springStrength = 50;
    public float springDamper = 4;

    [Header("Grip Settings")]
    [Range(0,1)] public float tireGripFactor = .8f;
    public float tireMass = .4f;

    [Header("Car Settings")]
    public float topSpeed = 10f;
    public AnimationCurve powerCurve;

    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Vector2 movementDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
     
        foreach (var wheel in frontWheels)
            wheel.AddComponent<FrontWheelBehaviour>().car = this;

        foreach (var wheel in rearWheels)
            wheel.AddComponent<WheelBehaviour>().car = this;
    }

}
