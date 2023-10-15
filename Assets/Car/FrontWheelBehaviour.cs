using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontWheelBehaviour : WheelBehaviour
{
    public float rotationTarget;
    float currentVelocity;

    private void FixedUpdate()
    {
        HandleForces();
        HandleSteering();
    }

    private void HandleSteering()
    {
        rotationTarget = car.movementDirection.x * car.maxWheelsTilt;
        float angle = Mathf.SmoothDampAngle(transform.localEulerAngles.y, rotationTarget, ref currentVelocity, car.tiltSmoothness);

        transform.localRotation = Quaternion.Euler(0, angle, 0);
    }
}
