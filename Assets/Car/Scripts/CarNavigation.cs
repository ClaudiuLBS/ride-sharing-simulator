using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarNavigation : MonoBehaviour
{
    public Transform target;
    public TextMeshProUGUI directionDisplay;
    public GameObject arrowDirection;

    private void Update()
    {
        CalculateDirection();    
    }

    private void CalculateDirection()
    {
        Transform target = Customers.Instance.startingPoint ?? Customers.Instance.finishingPoint;

        Vector2 targetDirection = new(target.position.x - transform.position.x, target.position.z - transform.position.z);
        directionDisplay.text = targetDirection.ToString();

        Vector2 forward = new Vector2(transform.forward.x, transform.forward.z);
        arrowDirection.transform.up = ReflectVector(forward, targetDirection.normalized);
    }

    Vector2 ReflectVector(Vector2 A, Vector2 B)
    {
        float dotProduct = Vector2.Dot(A, B);
        float denominator = B.sqrMagnitude;
        Vector2 reflection = A - 2 * (dotProduct / denominator) * B;
        return reflection;
    }
}
