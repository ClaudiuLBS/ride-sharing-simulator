using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    CarPhysics carPhysics;
    public bool isNearCustomer = false;
    public static TextMeshProUGUI helpText;

    public static event Action onCustomerPickup;
    public static event Action onCustomerDeliver;

    private void Awake()
    {
        carPhysics = GetComponent<CarPhysics>();
    }

    public void OnMove(InputValue value)
    {
        carPhysics.movementDirection = value.Get<Vector2>();
    }

    public void OnReset(InputValue _)
    {
        transform.Translate(0, 10, 0);
        transform.rotation = Quaternion.identity;
    }


    public void OnCustomerInteraction(InputValue _) 
    {
        print("Pressed button");
        if (isNearCustomer)
            onCustomerPickup?.Invoke();
    }


    private void OnTriggerEnter(Collider other)
    {
        isNearCustomer = true;
        helpText.text = $"Press E";
    }

    private void OnTriggerExit(Collider other)
    {
        isNearCustomer = false;
        helpText.text = "";
    }
}
