using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarNavigation : MonoBehaviour
{
    public Transform target;
    public TextMeshProUGUI directionDisplay;
    public GameObject directionalArrow;

    private void Update()
    {
        CalculateDirection();    
    }

    private void CalculateDirection()
    {
        Transform target = Customers.Instance.startingPoint ?? Customers.Instance.finishingPoint;

        var targetDirection = target.position - transform.position;
        directionDisplay.text = targetDirection.ToString();
        
        var arrowDirection = transform.InverseTransformDirection(targetDirection);
        directionalArrow.transform.up = new Vector2(arrowDirection.x, arrowDirection.z);
    }

}
