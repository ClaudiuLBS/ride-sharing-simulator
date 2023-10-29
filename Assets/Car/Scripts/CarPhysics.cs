using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics : MonoBehaviour
{
    [Header("Wheels Configuration")]
    [SerializeField] private GameObject[] frontWheels;
    [SerializeField] private GameObject[] rearWheels;
    public GameObject wheelPrefab;
    public float wheelHeight = .3f;
    public float tiltSmoothness = .5f;
    [Range(20, 70)] public float maxWheelsTilt = 50;
    
    [Header("Suspension Settings")]
    public float suspensionRestDistance = .7f;
    public float springStrength = 50;
    public float springDamper = 3;

    [Header("Grip Settings")]
    [Range(0,1)] public float tireGripFactor = .8f;
    public float tireMass = .4f;

    [Header("Car Settings")]
    public float topSpeed = 20f;
    public float horsePower = 90;
    public bool fwd;
    public bool rwd;
    public AnimationCurve powerCurve;

    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Vector2 movementDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
     
        foreach (var wheel in frontWheels)
        {
            var wb = wheel.AddComponent<FrontWheelBehaviour>();
            wb.car = this;
            wb.hasTraction = fwd;
        }

        foreach (var wheel in rearWheels)
        {
            var wb = wheel.AddComponent<WheelBehaviour>();
            wb.car = this;
            wb.hasTraction = rwd;
        }
    }

}
