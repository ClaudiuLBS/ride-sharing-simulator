using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    CarPhysics carPhysics;
    public bool isNearCustomer = false;
    public TextMeshProUGUI helpText;
    AudioSource audioSource;
    public static event Action onCustomerPickup;

    private void Awake()
    {
        carPhysics = GetComponent<CarPhysics>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (audioSource.pitch >= 1 && carPhysics.movementDirection.magnitude == 0)
            audioSource.pitch -= Time.deltaTime;

        else if (audioSource.pitch <= 1.5 && carPhysics.movementDirection.magnitude > 0)
            audioSource.pitch += Time.deltaTime * 0.1f;
    }

    public void OnMove(InputValue value)
    {
        carPhysics.movementDirection = value.Get<Vector2>();
        if (audioSource.pitch <= 1.5)
            audioSource.pitch += Time.deltaTime;
    }

    public void OnReset(InputValue _)
    {
        transform.Translate(0, 10, 0);
        transform.rotation = Quaternion.identity;
    }


    public void OnCustomerInteraction(InputValue _) 
    {
        if (isNearCustomer)
        {
            helpText.text = "";
            onCustomerPickup?.Invoke();
        }
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
