using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelBehaviour : MonoBehaviour
{
    public CarPhysics car;
    public Transform wheelObject;
    public bool onGround;

    protected float maxSuspensionDistance;
    protected Vector3 wheelWorldVelocity;
    protected RaycastHit rayHit;

    private void Start()
    {
        wheelObject = Instantiate(car.wheelPrefab, transform).transform;
        wheelObject.localPosition = Vector3.zero;
    }

    private void FixedUpdate()
    {
        HandleForces();
    }

    protected void HandleForces()
    {
        maxSuspensionDistance = car.suspensionRestDistance * 1.5f;
        onGround = Physics.Raycast(transform.position, -transform.up, out rayHit, maxSuspensionDistance);
        wheelWorldVelocity = car.rb.GetPointVelocity(transform.position);

        if (onGround) { 
            Debug.DrawRay(transform.position, -transform.up * rayHit.distance, Color.green);
            HandleSuspensionForce();
            HandleSteeringForce();
            HandleAccelerationForce();
        } 
        else
        {
            Debug.DrawRay(transform.position, -transform.up * maxSuspensionDistance, Color.red);
        }
    }

    private void HandleSuspensionForce()
    {
        Vector3 springDirection = transform.up;

        float offset = car.suspensionRestDistance - rayHit.distance;
        float velocity = Vector3.Dot(springDirection, wheelWorldVelocity);
        float force = (offset * car.springStrength) - (velocity * car.springDamper);

        //Adjust the wheels
        wheelObject.transform.position = rayHit.point + new Vector3(0, car.wheelHeight, 0); 

        car.rb.AddForceAtPosition(springDirection * force, transform.position);
    }

    private void HandleSteeringForce()
    {
        Vector3 steeringDirection = transform.right;
        
        float steeringVelocity = Vector3.Dot(steeringDirection, wheelWorldVelocity);
        float desiredVelocityChange = -steeringVelocity * car.tireGripFactor;
        float desiredAcceleration = desiredVelocityChange / Time.fixedDeltaTime;

        car.rb.AddForceAtPosition(steeringDirection * car.tireMass * desiredAcceleration, transform.position);
    }

    private void HandleAccelerationForce()
    {
        Vector3 accelerationDirection = transform.forward;

        float carSpeed = Vector3.Dot(car.transform.forward, car.rb.velocity);
        float normalizedCarSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / car.topSpeed);
        float availableTorque = car.powerCurve.Evaluate(normalizedCarSpeed) * car.movementDirection.y;

        wheelObject.Rotate(carSpeed, 0, 0);

        car.rb.AddForceAtPosition(accelerationDirection * availableTorque, transform.position);
    }

}
