using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    CarPhysics carPhysics;

    private void Awake()
    {
        carPhysics = GetComponent<CarPhysics>();
    }

    public void OnMove(InputValue value)
    {
        carPhysics.movementDirection = value.Get<Vector2>();
    }
}
