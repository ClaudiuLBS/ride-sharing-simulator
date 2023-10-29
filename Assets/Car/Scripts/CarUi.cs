using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarUi : MonoBehaviour
{
    public TextMeshProUGUI speedometerText;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var speed = Mathf.Round(rb.velocity.magnitude * 1.2f);
        speedometerText.text = speed.ToString() + " km/h";
    }
}
